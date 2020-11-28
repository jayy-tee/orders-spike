using System;
using System.Collections.Generic;

namespace Acme.Orders.Application.Model
{
    public class OrdersResult
    {
        public IEnumerable<OrderDto> Orders { get; set; }
        public bool IsLastPage { get; set; }
    }
}