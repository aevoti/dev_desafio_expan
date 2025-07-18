namespace DesafioAEVO.Communication.Responses
{
    public class OrderResponse
    {
        public Guid ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();

    }
}
