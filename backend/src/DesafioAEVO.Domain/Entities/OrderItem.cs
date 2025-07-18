namespace DesafioAEVO.Domain.Entities
{
    public class OrderItem
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public Product Product { get; set; }
    }
}
