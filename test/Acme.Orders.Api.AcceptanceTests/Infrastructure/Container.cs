using Microsoft.Extensions.DependencyInjection;
using Acme.Orders.TestSdk.Contracts;
using Acme.Orders.TestSdk.Infrastructure;
using Acme.Orders.TestSdk.Services;

namespace Acme.Orders.Api.AcceptanceTests.Infrastructure
{
    public static class Container
    {
        public static void Populate(IServiceCollection container, TestSettings testSettings)
        {
            container.AddScoped<IClient, Client>();
        }
    }
}
