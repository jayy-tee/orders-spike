using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Acme.Orders.Data
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<AcmeDbContext>
    {
        public AcmeDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<AcmeDbContext>();

            var connectionString = configuration
                .GetConnectionString("AcmeDb");
            
            dbContextBuilder.UseMySql(connectionString, b => b.MigrationsAssembly("Acme.Orders.Data"));


            return new AcmeDbContext(dbContextBuilder.Options);
        }
    }
}