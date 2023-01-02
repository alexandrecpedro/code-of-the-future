using Autofac;
using Autofac.Extensions.DependencyInjection;
using Basket.API.Model;
using Basket.API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RabbitMQ.Client;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Logging;
using Rebus.OpenTelemetry.Configuration;
using Rebus.Retry.Simple;
using Serilog;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IWebHostEnvironment env)
        {
            Configuration = configuration;

            var configurationByFile = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationByFile)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                        .WithOrigins(Configuration["SignalRServerUrl"])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true);
                }));

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = "Basket.API";
                    options.ApiSecret = "secret";
                    options.Authority = Configuration["IdentityUrl"];
                    options.RequireHttpsMetadata = false;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "The Grocery Store - Basket",
                    Description = "An API providing e-commerce basket functionalities.",
                    Contact = new OpenApiContact
                    {
                        Name = "Marcelo Oliveira",
                        Email = "mclricardo@gmail.com",
                        Url = new Uri("https://github.com/marcelooliveira")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licence XPTO 4567",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.ConfigureSwaggerGen(options =>
            {
                // UseFullTypeNameInSchemaIds replacement for .NET Core
                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            //By connecting here we are making sure that our service
            //cannot start until redis is ready. This might slow down startup,
            //but given that there is a delay on resolving the ip address
            //and then creating the connection it seems reasonable to move
            //that cost to startup instead of having the first request pay the
            //penalty.
            ConnectionMultiplexer redisConnection = null;
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<BasketConfig>>().Value;
                var configuration = ConfigurationOptions.Parse(Configuration["RedisConnectionString"], true);

                configuration.ResolveDns = true;

                redisConnection = ConnectionMultiplexer.Connect(configuration);
                return redisConnection;
            });

            services.AddTransient<IBasketRepository, RedisBasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>();

            services
                .AddSingleton<IConnection>(sp =>
                {
                    var factory = new ConnectionFactory()
                    {
                        Uri = new Uri(Configuration["RabbitMQConnectionString"]),
                        AutomaticRecoveryEnabled = true
                    };

                    return factory.CreateConnection();
                })
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddRedis(
                    Configuration["RedisConnectionString"],
                    name: "redis-check",
                    tags: new string[] { "redis" })
                .AddRabbitMQ();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            ConfigureRebus(services);

            services.AddOpenTelemetryTracing(builder =>
                builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Basket.API"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRebusInstrumentation()
                .AddRedisInstrumentation(redisConnection)
                .AddJaegerExporter());

        }

        private void ConfigureRebus(IServiceCollection services)
        {
            // Configure and register Rebus
            services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(Configuration["RabbitMQConnectionString"], Configuration["RabbitMQInputQueueName"]))
                .Options(o => o.EnableDiagnosticSources())); // This is the important line
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Grocery Store - Basket v1");
            });

            app.UseAuthentication();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.ApplicationServices.UseRebus();
        }
    }
}
