using Acme.Orders.TestSdk.RequestBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using Acme.Orders.Application.Common;
using Acme.Orders.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;


namespace Acme.Orders.Api.AcceptanceTests
{
    [TestClass]
    [TestCategory("RequiresInProcess")]
    public class InProcessOrderTests : OrderTestBase
    {
        private string _orderId;
        protected override bool IsInProcessOnly => true;
        protected override void ConfigureServices(IServiceCollection services)
        {
            var existingDescriptors = services.Where(s => s.ServiceType == typeof(AcmeDbContext));
            existingDescriptors.ToList().ForEach(descriptor =>
            {
                services.Remove(descriptor);
            });

            services.AddDbContext<AcmeDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "testDatabase"));
        }

        [TestInitialize]
        public void WhenRunningTests_CreateAnOrderIfItDoesntExist()
        {
            // Create a new order.
            SetupTest();

            if (null != _orderId)
                return;

            var request = new OrderRequestBuilder()
                .CreateOrder()
                .Build();

            var response = Client.Execute(request, andExpect: System.Net.HttpStatusCode.Created);
            response.Content.Should().NotBeEmpty();
            _orderId = response.Content;
        }

        [TestMethod]
        public void WhenPlacingAnEmptyOrder_WeGetBadRequest()
        {
            // Arrange
            var request = new OrderRequestBuilder()
                    .Order(withOrderId: _orderId)
                    .PlaceOrder()
                    .Build();

            // Act/Assert
            Client.Execute(request, andExpect: System.Net.HttpStatusCode.BadRequest);
        }
    }
}
