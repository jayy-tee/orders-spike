using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Orders.TestSdk.RequestBuilders;
using Acme.Orders.TestSdk.ResponseModels;
using System.Text.Json;
using System.Net;
using Acme.Orders.TestSdk.Models;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using RestSharp;

namespace Acme.Orders.Api.AcceptanceTests
{
    [TestClass]
    public class HealthTests : AcceptanceTestBase
    {

        [TestMethod]
        public void WhenWeAccessTheLiveEndpoint_WeGetAnOKResponse()
        {
            var request = new Request
            {
                Method = Method.GET,
                RelativeUrl = "/health/live"
            };

            Client.Execute(request, andExpect: HttpStatusCode.OK);
        }
        
        [TestMethod]
        public void WhenWeAccessTheReadyEndpoint_WeGetAnOKResponse()
        {
            var request = new Request
            {
                Method = Method.GET,
                RelativeUrl = "/health/ready"
            };

            Client.Execute(request, andExpect: HttpStatusCode.OK);
        }
    }
}
