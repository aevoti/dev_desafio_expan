namespace ECommerce.Domain;

public class Order {
    public Guid Id { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}