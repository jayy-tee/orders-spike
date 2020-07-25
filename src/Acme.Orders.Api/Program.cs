using Acme.Orders.Api.Config;
using Acme.Orders.Api.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Acme.Orders.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = LoggingExtensions.BuildLogger(ConfigurationSingleton.Instance);

            try
            {
                Log.Information("Starting Host...");
                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder() => CreateHostBuilder(null);
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder => BuildConfig(builder))
                .ConfigureAppConfiguration((context, builder) => BuildConfig(builder))
                .UseCustomSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IConfigurationBuilder BuildConfig(IConfigurationBuilder builder) 
            => builder.AddConfiguration(ConfigurationSingleton.Instance);
    }
}
