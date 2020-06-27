using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Common
{
    public interface IAcmeDbContext
    {
        DbSet<Order> Orders { get; set; }

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
