namespace DesafioAEVO.Communication.Responses
{
    public class ProductResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
