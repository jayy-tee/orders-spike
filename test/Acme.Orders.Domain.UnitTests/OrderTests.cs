using System.Linq;
using Acme.Orders.Domain.Entities;
using Acme.Orders.Domain.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Acme.Orders.Domain.UnitTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void WhenAnItemIsAdded_CostIsCorrect()
        {
            var order = new Order();
            order.AddItem(new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            });

            order.Items.First().Cost.Should().Be(5.50M);
        }

        [TestMethod]
        public void WhenAnOrderIsPlaced_ItemsCannotBeAdded()
        {
            var order = new Order();
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };

            order.AddItem(itemToAdd);
            order.Place();

            Assert.ThrowsException<OrdersDomainException>(() => order.AddItem(itemToAdd));
        }

        [TestMethod]
        public void WhenAnOrderIsPlaced_ItemsCannotBeRemoved()
        {
            var order = new Order();
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };

            order.AddItem(itemToAdd);
            order.Place();

            Assert.ThrowsException<OrdersDomainException>(() => order.RemoveItem(itemToAdd));
        }

        [TestMethod]
        public void WhenAnOrderIsPlaced_ItCannotBePlacedAgain()
        {
            var order = new Order();
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };

            order.AddItem(itemToAdd);
            order.Place();

            Assert.ThrowsException<OrdersDomainException>(() => order.Place());
        }

        [TestMethod]
        public void WhenAnEmptyOrderIsPlaced_ExceptionIsThrown()
        {
            var order = new Order();
            
            Assert.ThrowsException<OrdersDomainException>(() => order.Place());
        }
    }
}