using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using MVC.Commands;
using MVC.Model.Redis;
using MVC.SignalR;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using Services;
using StackExchange.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC
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

        public void ConfigureServices(IServiceCollection services)
        {
            var uri = new Uri(Configuration["ApiUrl"]);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = uri
            };

            services.AddSingleton(typeof(HttpClient), httpClient);
            services.AddHttpContextAccessor();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<ISessionHelper, SessionHelper>();
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddRedis(
                    Configuration["RedisConnectionString"],
                    name: "redis-check",
                    tags: new string[] { "redis" });

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            //services.AddMvc()
            //    .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllersWithViews()
                .AddNewtonsoftJson();

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
                var configuration = ConfigurationOptions.Parse(Configuration["RedisConnectionString"], true);

                configuration.ResolveDns = true;

                redisConnection = ConnectionMultiplexer.Connect(configuration);
                return redisConnection;
            });
            services.AddAuthorization();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = Configuration["IdentityUrl"];
                    options.BackchannelHttpHandler = new HttpClientHandler() { Proxy = new WebProxy() };
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "MVC";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Events = new OpenIdConnectEvents()
                    {
                        OnUserInformationReceived = (context) =>
                        {
                            if (!(context.Principal.Identity is ClaimsIdentity claimsId))
                            {
                                throw new Exception();
                            }

                            return Task.CompletedTask;
                        }
                    };

                    options.Scope.Add("Basket.API");
                    options.Scope.Add("Ordering.API");
                    options.Scope.Add("offline_access");
                });
            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddTransient<IUserRedisRepository, UserRedisRepository>();
            services.AddMediatR(typeof(UserNotificationCommand).GetTypeInfo().Assembly);

            services.AddHttpClient<IBasketService, BasketService>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<ICatalogService, CatalogService>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IOrderService, OrderService>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddOpenTelemetryTracing(builder =>
                builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MVC"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation(
                    // we can hook into existing activities and customize them
                    options => options.Enrich = (activity, eventName, rawObject) =>
                    {
                        if (eventName == "OnStartActivity" && rawObject is System.Net.Http.HttpRequestMessage request)
                        {
                            activity.SetTag("RandomDemoTag", "Adding some random demo tag, just to see things working");
                        }
                    }
                )
                .AddRedisInstrumentation(redisConnection)
                .AddJaegerExporter());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseSession();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{code?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserCounterDataHub>("/usercounterdatahub",
                    options =>
                    {
                        options.Transports = HttpTransportType.WebSockets;
                    });
            });
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(60),
                onBreak: (a,b) =>
                {
                    Console.WriteLine();
                    Console.WriteLine("==============================", ConsoleColor.Red);
                    Console.WriteLine("Polly CircuitBreaker: ON BREAK", ConsoleColor.Red);
                    Console.WriteLine("==============================", ConsoleColor.Red);
                    Console.WriteLine("O circuito Polly.CircuitBreaker estava FECHADO e mudou para o estado ABERTO.", ConsoleColor.Red);
                    Console.WriteLine();
                },
                onReset: () =>
                {
                    Console.WriteLine();
                    Console.WriteLine("===============================");
                    Console.WriteLine("Polly CircuitBreaker: ON RESET");
                    Console.WriteLine("===============================");
                    Console.WriteLine("O circuito Polly.CircuitBreaker estava ABERTO e mudou para o estado FECHADO.", ConsoleColor.Green);
                    Console.WriteLine("");
                    Console.WriteLine();
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine();
                    Console.WriteLine("==================================");
                    Console.WriteLine("Polly CircuitBreaker: ON HALF OPEN");
                    Console.WriteLine("==================================");
                    Console.WriteLine("O circuito Polly.CircuitBreaker estava ABERTO e mudou para o estado SEMIABERTO e está aceitando novas requisições.", ConsoleColor.Yellow);
                    Console.WriteLine("");
                    Console.WriteLine();
                });
        }
    }
}
