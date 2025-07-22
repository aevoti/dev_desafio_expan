namespace DesafioAEVO.Communication.Responses
{
    public class CreateOrderResponse
    {
        public Guid OrderID { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}
