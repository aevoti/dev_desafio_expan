using DesafioAEVO.Domain.Enums;

namespace DesafioAEVO.Domain.Entities
{
    public class Order
    {
        public Guid ID { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(-3);
        public OrderStatus Status { get; set; } = OrderStatus.Received;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public Order()
        {
            ID = Guid.NewGuid();
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}
