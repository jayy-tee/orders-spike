using System.Linq;
using Acme.Orders.Application.Model;

namespace Acme.Orders.Api.Responses
{
    public static class OrdersResponseMapper
    {
        public static OrdersResponse MapToResponseModel(this OrdersResult ordersResult)
        {
            return new OrdersResponse
            {
                Orders = ordersResult.Orders,
                Count = ordersResult.Orders.Count
            };
        }
    }
}