using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Acme.Orders.Api.Logging
{
    /*** 
    -------------------------------------------------
    Custom Exception Handling for Business Exceptions
    -------------------------------------------------
    Refer:
    https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-excluding-health-check-endpoints-from-serilog-request-logging/
    
    ***/

    public static class LogHelper
    {
        private static List<string> _excludedEndpoints = new List<string> { "/health/ready", "/health/ok", "/" };
        public static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double _, Exception ex) =>
            ex != null
                ? LogEventLevel.Error
                : ctx.Response.StatusCode > 499
                    ? LogEventLevel.Error
                    : IsHealthCheckEndpoint(ctx) // Not an error, check if it was a health check
                        ? LogEventLevel.Verbose // Was a health check, use Verbose
                        : LogEventLevel.Information;

        private static bool IsHealthCheckEndpoint(HttpContext ctx) => _excludedEndpoints.Contains(ctx.Request.Path);
    }
}
