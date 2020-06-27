using Acme.Orders.Common.Enums;
using Acme.Orders.Common.ValueObjects;

namespace Acme.Orders.Domain.Entities
{
    public class CustomerAddress
    {
        public AddressType Type { get; set; }
        public Address Address { get; set; }
    }
}
