using System.Net;

namespace Acme.Orders.TestSdk.Contracts
{
    public interface IResponse
    {
        HttpStatusCode StatusCode { get; }
        string Content { get; }
        T As<T>() where T : class;
    }
}
