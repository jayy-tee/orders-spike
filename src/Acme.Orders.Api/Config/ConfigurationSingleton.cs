using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Acme.Orders.Api.Config
{
    public class ConfigurationSingleton
    {
        public const string EnvironmentVariablePrefix = "ACME";
        private static IConfiguration _instance;
        private static readonly object _padlock = new object();


        public static IConfiguration Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (null != _instance)
                    {
                        return _instance;
                    }

                    _instance = GetBuilder().Build();

                    return _instance;
                }
            }
        }

        protected static IConfigurationBuilder GetBuilder()
        {
            var result = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("serilogSettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddJsonFile($"serilogSettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables(EnvironmentVariablePrefix);
            
            return result;
        }            
    }
}
