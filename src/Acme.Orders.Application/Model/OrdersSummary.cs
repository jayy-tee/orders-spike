using System.Collections.Generic;
using Acme.Orders.Common.Enums;

namespace Acme.Orders.Application.Model
{
    public class OrdersSummary
    {
        public Dictionary<string, int> OrderStatuses { get; set; }

        public class OrderStatusSummary
        {
            public OrderStatus Status { get; set; }
            public int Count { get; set; }
        }
    }
    
    
}