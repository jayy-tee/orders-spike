using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Orders.TestSdk.RequestBuilders;
using Acme.Orders.TestSdk.ResponseModels;
using System.Text.Json;
using System.Net;

namespace Acme.Orders.Api.AcceptanceTests
{
    [TestClass]
    public class OrderTests : OrderTestBase
    {
        protected override bool SkipIfInProcess => true;

        [TestMethod]
        public void BogusTestThatShouldBeSkippedWhenInProcess()
        {
            Assert.IsTrue(true);
        }
    }
}
