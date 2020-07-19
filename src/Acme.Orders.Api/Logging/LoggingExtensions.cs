using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;


namespace Acme.Orders.Api.Logging
{
    public static class LoggingExtensions
    {
        public static ILogger BuildLogger(IConfiguration configuration)
        {
            return BuildLoggerConfiguration(configuration)
                .CreateLogger();
        }

        public static IHostBuilder UseCustomSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.FromLogContext(), preserveStaticLogger:false);
        
            return hostBuilder;
        }

        private static LoggerConfiguration BuildLoggerConfiguration(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext();
        }
    }
}
