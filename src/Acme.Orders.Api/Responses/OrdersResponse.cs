using System.Collections.Generic;
using Acme.Orders.Application.Model;

namespace Acme.Orders.Api.Responses
{
    public class OrdersResponse
    {
        public int Count { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
        public string NextPage { get; set; }
    }
}