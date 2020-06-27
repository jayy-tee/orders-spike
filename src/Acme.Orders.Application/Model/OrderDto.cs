using System;

namespace Acme.Orders.Application.Model
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
