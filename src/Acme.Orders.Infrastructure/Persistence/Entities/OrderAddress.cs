using Acme.Orders.Domain.Entities;

namespace Acme.Orders.Infrastructure.Persistence.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        
        public Order Order { get; set; }
    }
}
