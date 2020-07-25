using System.Text.Json;
using RestSharp;
using System.Net;
using Acme.Orders.TestSdk.Contracts;

namespace Acme.Orders.TestSdk.Models
{
    public class Response : IResponse
    {
        private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false,
        };
        private readonly IRestResponse _response;

        public Response(IRestResponse response)
        {
            _response = response;
        }

        public HttpStatusCode StatusCode => _response.StatusCode;

        public string Content => _response.Content;

        public T As<T>() where T : class
        {
            return JsonSerializer.Deserialize<T>(_response.Content, _jsonOptions);
        }
    }
}
