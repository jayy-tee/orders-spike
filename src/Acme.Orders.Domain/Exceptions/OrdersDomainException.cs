using System;

namespace Acme.Orders.Domain.Exceptions
{
    public class OrdersDomainException : Exception
    {
        public OrdersDomainException()
        {
        }

        public OrdersDomainException(string message) : base(message)
        {
        }
    }
}