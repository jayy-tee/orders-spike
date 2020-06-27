using System.Collections.Generic;
using Acme.Orders.Domain.Entities;

namespace Acme.Orders.Application.Model.Extensions
{
    public static class OrderExtensions
    {
        public static Order WithItems(this Order theOrder, IEnumerable<OrderItem> theItems)
        {
            theOrder.AddItems(theItems);

            return theOrder;
        }

        public static OrderItem ToDomainModel(this OrderItemDto orderItem)
        {
            return new OrderItem
            {
                ProductCode = orderItem.ProductCode,
                Description = orderItem.Description,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity
            };
        }
    }
}
