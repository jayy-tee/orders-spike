namespace Acme.Orders.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get;  set; }
        public string ProductCode { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get;  set; }
        public int Quantity { get; set; }
        public decimal Cost  => Quantity * Price;

        public Order Order { get; set; }
    }
}
