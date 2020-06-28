using System.Linq;
using Acme.Orders.Domain.Entities;
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
            order.AddItem(new OrderItem{
                Price = 5.50M,
                Quantity = 1
            });

            order.Items.First().Cost.Should().Be(5.50M);
        }
    }
}
