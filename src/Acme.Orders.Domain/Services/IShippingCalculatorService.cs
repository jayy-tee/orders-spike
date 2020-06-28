using Acme.Orders.Common.ValueObjects;


namespace Acme.Orders.Domain.Services
{
    public interface IShippingCalculatorService
    {
        decimal CalculateShippingCost(Address shippingAddress);
    }
}
