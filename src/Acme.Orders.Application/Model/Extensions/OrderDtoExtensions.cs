using System.Collections.Generic;
using Acme.Orders.Domain.Entities;
using Acme.Orders.Common.Enums;

namespace Acme.Orders.Application.Model.Extensions
{
    public static class OrderDtoExtensions
    {
        public static OrderDto MapToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                DateCreated = order.DateCreated,
                DateUpdated = order.DateUpdated,
                Status = order.Status.ToString(),
                Total = order.Total,
                ShippingCost = order.ShippingCost,

            };
        }

        public static OrderItemDto MapToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                Id = orderItem.Id,
                ProductCode = orderItem.ProductCode,
                Description = orderItem.Description,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
                Cost = orderItem.Cost,
            };
        }
    }
}
