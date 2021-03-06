using Acme.Orders.Api.Exceptions;
using Acme.Orders.Api.Filters;
using Acme.Orders.Api.Logging;
using Acme.Orders.Application;
using Acme.Orders.Data;
using Acme.Orders.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Acme.Orders.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });
            services.AddControllers(options =>
                options.Filters.Add(typeof(ApiExceptionFilter))
            );
            services.AddApplication();
            services.AddData(Configuration);
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(c => c.UseGlobalExceptionHandler(env));
            app.UseSerilogRequestLogging(opts =>
            {
                opts.GetLevel = LogHelper.ExcludeHealthChecks;
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
