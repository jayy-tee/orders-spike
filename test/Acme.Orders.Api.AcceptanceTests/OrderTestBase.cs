using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Orders.TestSdk.RequestBuilders;
using Acme.Orders.TestSdk.ResponseModels;
using System;
using System.Net;
using System.Text.Json;

namespace Acme.Orders.Api.AcceptanceTests
{
    [TestClass]
    public abstract class OrderTestBase : AcceptanceTestBase
    {
    
        public virtual void SetupTest()
        {

        }
        
        [TestMethod]
        public void WhenAnOrderDoesntExist_WeGetNotFound()
        {
            // Arrange
            var nonExistentOrder = Guid.NewGuid().ToString();
            var request = new OrderRequestBuilder()
                .GetOrder(withOrderId: nonExistentOrder)
                .Build();

            // Act
            Client.Execute(request, andExpect: System.Net.HttpStatusCode.NotFound);
        }
        
        
        
    }
}
