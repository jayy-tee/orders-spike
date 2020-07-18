using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Api.Exceptions
{
    public static class GlobalExceptionHandler
    {
        /*** 
          -------------------------------------------------
          Custom Exception Handling for Business Exceptions
          -------------------------------------------------
          Refer https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
          
        ***/
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next) => 
            ExceptionResponseHelper.HandleException(httpContext, false);
        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next) => 
            ExceptionResponseHelper.HandleException(httpContext, true);
    }
}
