using System;
using System.Text;
using Acme.Orders.TestSdk.Contracts;
using Acme.Orders.TestSdk.Models;
using Acme.Orders.TestSdk.RequestModels;
using System.Text.Json;
using RestSharp;

namespace Acme.Orders.TestSdk.RequestBuilders
{
    public class OrderRequestBuilder
    {
        private Method _method;
        private string _relativeUrl;
        private string _orderId;

        public OrderRequestBuilder()
        {
            _method = Method.GET;
            _relativeUrl = "/fake/url";
        }

        public OrderRequestBuilder Order(string withOrderId)
        {
            _orderId = withOrderId;
            return this;
        }

        public OrderRequestBuilder CreateOrder()
        {
            _method = Method.POST;
            _relativeUrl = "/orders/new";
            
            return this;
        }

        public OrderRequestBuilder GetOrder(string withOrderId)
        {
            _method = Method.GET;
            _relativeUrl = new StringBuilder("/orders/")
                .Append(withOrderId)
                .ToString();

            return this;
        }

        public OrderRequestBuilder PlaceOrder()
        {
            _method = Method.POST;
            _relativeUrl = new StringBuilder("/orders/")
                .Append(_orderId)
                .Append("/place")
                .ToString();

            return this;
        }
        
        public OrderRequestBuilder HavingOrderId(string orderId)
        {
            

            return this;
        }
        
        


        public IRequest Build()
        {
            return new Request()
            {
                Method = _method,
                RelativeUrl = _relativeUrl,
                Body = null
            };
        }
    }
}