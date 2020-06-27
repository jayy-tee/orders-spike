using System;

namespace Acme.Orders.Common.ValueObjects
{
    public class Address
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            // TODO : return string representation via StringBuilder
            return String.Empty;
        }

    }
}
