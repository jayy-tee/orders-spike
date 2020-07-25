using System;
using Acme.Orders.Common.ValueObjects;
using Acme.Orders.Domain.Services;

namespace Acme.Orders.Domain.UnitTests.Fakes
{
    public class ShippingCalculatorFake : IShippingCalculatorService
    {
        public readonly decimal ShippingCost = (decimal) ((new Random()).NextDouble() * 1.32333);
        public decimal CalculateShippingCost(Address shippingAddress)
        {
            return ShippingCost;
        }
    }
}
