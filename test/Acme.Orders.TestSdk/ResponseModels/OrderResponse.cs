namespace Acme.Orders.TestSdk.ResponseModels
{
    public class OrderResponse
    {
        public ulong id { get; set; }
        public string dateCreated { get; set; }
        public string dateUpdated { get; set; }
        public string status { get; set; }
        public decimal total { get; set; }
        public decimal shippingCost { get; set; }
    }
}