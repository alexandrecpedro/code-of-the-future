using HealthChecks.UI.Client;
using Identity.API.Commands;
using Identity.API.Data;
using Identity.API.IntegrationEvents.EventHandling;
using Identity.API.Managers;
using Identity.API.Models;
using IdentityServer4.Services;
using MediatR;
using Messages.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Rebus.Config;
using Rebus.ServiceProvider;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Identity.API
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment environment)
        {

            Configuration = configuration;
            Environment = environment;
            _loggerFactory = loggerFactory;

            var configurationByFile = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationByFile)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
            services.AddScoped<IClaimsManager, ClaimsManager>();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            //services.AddMvc();

            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton<IProfileService, ProfileService>();

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients(Configuration["CallbackUrl"]))
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();

            builder.Services.ConfigureExternalCookie(options => {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            builder.Services.ConfigureApplicationCookie(options => {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddScoped<IMediator, NoMediator>();
            services.AddScoped<IRequest<bool>, RegistrationCommand>();
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlite(Configuration.GetConnectionString("DefaultConnection"))
                .AddRabbitMQ(Configuration["RabbitMQConnectionString"]);

            services.AddMediatR(typeof(RegistrationCommand).GetTypeInfo().Assembly);

            RegisterRebus(services);

            services.AddOpenTelemetryTracing(builder =>
                builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Identity.API"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddJaegerExporter());
        }

        private void RegisterRebus(IServiceCollection services)
        {
            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<RegistrationEventHandler>();

            // Configure and register Rebus
            services.AddRebus(configure => configure
                .Logging(l => l.Use(new MSLoggerFactoryAdapter(_loggerFactory)))
                .Transport(t => t.UseRabbitMq(Configuration["RabbitMQConnectionString"], Configuration["RabbitMQInputQueueName"])))
                .AddTransient<DbContext, ApplicationDbContext>()
                .AutoRegisterHandlersFromAssemblyOf<RegistryEvent>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseRebus(
                async (bus) =>
                {
                    await bus.Subscribe<RegistryEvent>();
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
            app.UseIdentityServer();
            //app.UseMvcWithDefaultRoute();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
