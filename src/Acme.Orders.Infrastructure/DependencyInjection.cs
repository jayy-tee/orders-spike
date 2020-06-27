using Acme.Orders.Application.Common;
using Acme.Orders.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Acme.Orders.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcmeDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "testDatabase"));

            services.AddScoped<IAcmeDbContext>(provider => provider.GetService<AcmeDbContext>());
            return services;
        }
    }
}
