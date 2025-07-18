using DesafioAEVO.Domain.Entities;
using DesafioAEVO.Infrastructure.DataAccess;

namespace DesafioAEVO.Infrastructure.DataSeeder
{
    public static class DataSeeder
    {
        public static void SeedInitialData(DesafioAEVOdbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product("Notebook Gamer AEVO XPS", 6500),
                    new Product("Mouse AEVO sem fio", 350),
                    new Product("Teclado Mecânico AEVO", 480),
                    new Product("Monitor Gamer AEVO Full HD", 1080),
                    new Product("Headset Gamer AEVO Surround", 720),
                    new Product("Webcam HD AEVO", 400),
                    new Product("Cadeira Gamer AEVO Confort", 1500),
                    new Product("SSD NVMe AEVO 1TB", 900),
                    new Product("Placa de Vídeo AEVO RTX 4070", 4200),
                    new Product("Fonte Modular AEVO 650W", 650)
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
