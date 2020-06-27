namespace Acme.Orders.Application.Model
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }
}
