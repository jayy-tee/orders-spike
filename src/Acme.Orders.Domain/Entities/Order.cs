using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Orders.Common.Enums;
using Acme.Orders.Common.ValueObjects;
using Acme.Orders.Domain.Services;

namespace Acme.Orders.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset DateUpdated { get; private set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; private set; } = OrderStatus.New;
        public decimal Total { get; private set; }
        public decimal ShippingCost { get; private set; }
        public Address ShippingAddress { get; private set; } 
        public IReadOnlyCollection<OrderItem> Items => _items;

        private readonly List<OrderItem> _items = new List<OrderItem>();

        public Order() { }

        public void AddItems(IEnumerable<OrderItem> theItems)
        {
            theItems.ToList().ForEach(i => AddItem(i));
        }

        public void AddItem(OrderItem theItem)
        {
            _items.Add(theItem);
            UpdateOrder();
        }

        public void RemoveItem(OrderItem theItem)
        {
            _items.Remove(theItem);
            UpdateOrder();
        }

        public void CalculateShipping(IShippingCalculatorService shippingcalculator)
        {
            ShippingCost = shippingcalculator.CalculateShippingCost(ShippingAddress);
        }

        private void UpdateOrder()
        {
            DateUpdated = DateTimeOffset.Now;
        }
    }
}
