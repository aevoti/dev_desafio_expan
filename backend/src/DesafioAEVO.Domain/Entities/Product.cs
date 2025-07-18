namespace DesafioAEVO.Domain.Entities
{
    public class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        private Product() { }

        public Product(string name, decimal price)
        {
            ID = Guid.NewGuid();
            Name = name;
            Price = price;
        }
    }
}
