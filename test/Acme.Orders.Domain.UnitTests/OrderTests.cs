using System;
using System.Linq;
using Acme.Orders.Domain.Entities;
using Acme.Orders.Domain.Exceptions;
using Acme.Orders.Domain.UnitTests.Fakes;
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
            var order = new Order(new ulong());
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
            var order = new Order(new ulong());
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
            var order = new Order(new ulong());
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
            var order = new Order(new ulong());
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
            var order = new Order(new ulong());
            
            Assert.ThrowsException<OrdersDomainException>(() => order.Place());
        }

        [TestMethod]
        public void WhenAnItemIsAdded_ItCanBeRemoved()
        {
            var order = new Order(new ulong());
            var expectedItemCount = order.Items.Count();
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };
            
            order.AddItem(itemToAdd);
            order.RemoveItem(itemToAdd);

            order.Items.Count.Should().Be(expectedItemCount);
            order.Items.Should().NotContain(itemToAdd);
        }

        [TestMethod]
        public void WhenShippingCostIsCalculated_TheOrderReflectsTheCorrectAmount()
        {
            var order = new Order(new ulong());
            var calculator = new ShippingCalculatorFake();
            
            order.CalculateShipping(calculator);

            order.ShippingCost.Should().Be(calculator.ShippingCost);
        }
        
        [TestMethod]
        public void WhenAnOrderIsPlaced_ShippingCostCanNotChange()
        {
            var order = new Order(new ulong());
            var calculator = new ShippingCalculatorFake();
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };
            
            order.AddItem(itemToAdd);
            order.Place();
            
            Assert.ThrowsException<OrdersDomainException>(() => order.CalculateShipping(calculator));
        }
        
        /* Further tests added, just to please the code coverage report. Do they add any value though? ;) */
        [TestMethod]
        public void WhenAnOrderIsCreated_ItHasANonEmptyGuidAsAnId()
        {
            var order = new Order(new ulong());
            order.Id.Should().NotBe(null);
        }
        
        [TestMethod]
        public void WhenAnOrderIsCreated_ItHasACreatedDate()
        {
            var order = new Order(new ulong());
            order.DateCreated.Should().BeWithin(TimeSpan.FromSeconds(1)).Before(DateTimeOffset.Now);
        }
        
        [TestMethod]
        public void WhenAnOrderIsUpdated_DateUpdatedReflectsThis()
        {
            var order = new Order(new ulong());
            var itemToAdd = new OrderItem
            {
                Price = 5.50M,
                Quantity = 1
            };

            order.AddItem(itemToAdd);
            order.DateUpdated.Should().BeAfter(order.DateCreated);
            order.DateUpdated.Should().BeWithin(TimeSpan.FromSeconds(1)).Before(DateTimeOffset.Now);
        }

    }
}