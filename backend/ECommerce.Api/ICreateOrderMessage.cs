public interface ICreateOrderMessage
{
    Guid OrderId { get; }
    DateTime CreatedAt { get; }
    List<OrderItemDto> Items { get; }
}
