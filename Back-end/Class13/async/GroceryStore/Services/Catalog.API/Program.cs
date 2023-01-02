using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "Catalog.API";

            IWebHost host = BuildWebHost(args);
            await SeedData.EnsureSeedData(host.Services);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                    .CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
