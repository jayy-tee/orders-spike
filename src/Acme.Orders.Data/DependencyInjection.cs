using Acme.Orders.Application.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcmeDbContext>(
                options => options.UseMySql(
                    configuration.GetConnectionString("AcmeDb"),
                    b => b.MigrationsAssembly("Acme.Orders.Data"))); 

            services.AddScoped<IAcmeDbContext>(provider => provider.GetService<AcmeDbContext>());
            
            return services;
        }
    }
}
