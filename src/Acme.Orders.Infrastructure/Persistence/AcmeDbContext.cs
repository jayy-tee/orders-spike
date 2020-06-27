using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Infrastructure.Persistence
{
    public class AcmeDbContext : DbContext, IAcmeDbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AcmeDbContext(DbContextOptions<AcmeDbContext> options)
                    : base(options)
        {
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await this.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcmeDbContext).Assembly);
        }
    }
}

