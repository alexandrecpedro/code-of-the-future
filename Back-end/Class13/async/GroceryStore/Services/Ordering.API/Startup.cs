using AutoMapper;
using HealthChecks.UI.Client;
using MediatR;
using Messages.EventHandling;
using Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ordering.API.SignalR;
using Ordering.Commands;
using Ordering.Repositories;
using RabbitMQ.Client;
using Rebus.Config;
using Rebus.OpenTelemetry.Configuration;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Ordering
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {

            Configuration = configuration;
            _loggerFactory = loggerFactory;

            var configurationByFile = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
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
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            //services.AddHealthChecks()
            //    .AddCheck("self", () => HealthCheckResult.Healthy())
            //    .AddSqlServer(Configuration["ConnectionString"],
            //        name: "Ordering DB Check",
            //        tags: new string[] { "OrderingDB" })
            //    .AddRabbitMQ(Configuration["RabbitMQConnectionString"], HealthStatus.Healthy);

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
                .AddSqlServer(Configuration["ConnectionString"],
                    name: "Ordering DB Check",
                    tags: new string[] { "OrderingDB" })
                .AddRabbitMQ();

            services.AddAutoMapper();

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = "Ordering.API";
                    options.ApiSecret = "secret";
                    options.Authority = Configuration["IdentityUrl"];
                    //options.BackchannelHttpHandler = new HttpClientHandler() { Proxy = new WebProxy(Configuration["System:Proxy"]) };
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
                    options.SaveToken = true;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "The Grocery Store - Ordering API",
                    Description = "Uma API contendo funcionalidades da aplicação de e-Commerce:" +
                    "Criação de orders.",
                    TermsOfService = "Nenhum",
                    Contact = new Contact
                    {
                        Name = "Marcelo Oliveira",
                        Email = "mclricardo@gmail.com",
                        Url = "https://twitter.com/twmoliveira"
                    },
                    License = new License
                    {
                        Name = "Licença XPTO 4567",
                        Url = "https://example.com/license"
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

            services.AddDistributedMemoryCache();
            services.AddSession();

            string connectionString = Configuration["ConnectionString"];

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddScoped<DbContext, ApplicationContext>();
            var serviceProvider = services.BuildServiceProvider();
            var contexto = serviceProvider.GetService<ApplicationContext>();
            services.AddSingleton<ApplicationContext>(contexto);

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IMediator, NoMediator>();
            services.AddScoped<IRequest<bool>, CreateOrderCommand>();
            services.AddMediatR(typeof(CreateOrderCommand).GetTypeInfo().Assembly);
            RegisterRebus(services);

            services.AddOpenTelemetryTracing(builder =>
                builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Ordering.API"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                .AddRebusInstrumentation()
                .AddJaegerExporter());

            services.AddDistributedMemoryCache();
        }

        private void RegisterRebus(IServiceCollection services)
        {
            // Configure and register Rebus
            services.AddRebus(configure => configure
                    .Transport(t => t.UseRabbitMq(Configuration["RabbitMQConnectionString"], Configuration["RabbitMQInputQueueName"]))
                    .Options(o => o.EnableDiagnosticSources()) // This is the important line
                    );
            services.AutoRegisterHandlersFromAssemblyOf<CheckoutEventHandler>();
        }

        public void Configure(IServiceProvider serviceProvider, IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Grocery Store - Ordering v1");
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.ApplicationServices.UseRebus(
                async (bus) =>
                {
                    await bus.Subscribe<CheckoutEvent>();
                });
        }
    }
}
