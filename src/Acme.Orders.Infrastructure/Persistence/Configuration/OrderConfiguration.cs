using Acme.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Northwind.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Items).WithOne(i => i.Order);
            builder.Ignore(e => e.ShippingAddress);
        }
    }
}
