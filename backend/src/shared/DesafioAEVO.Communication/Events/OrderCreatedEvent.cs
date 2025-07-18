namespace DesafioAEVO.Communication.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; } = "Received";
        public List<OrderItemMessage> Items { get; set; } = new();
    }
    public class OrderItemMessage
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
