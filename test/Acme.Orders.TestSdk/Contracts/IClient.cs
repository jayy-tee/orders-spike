using System.Net;

namespace Acme.Orders.TestSdk.Contracts
{
    public interface IClient
    {
        IResponse Execute(IRequest request);
        IResponse Execute(IRequest request, HttpStatusCode andExpect);
    }
}
