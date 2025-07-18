using DesafioAEVO.Domain.Abstractions.Repositories;
using DesafioAEVO.Domain.Entities;
using Moq;
using System.Collections.Concurrent;

namespace CommonTestUtilities.Repositories
{
    public class ProductRepositoryBuilder
    {
        private readonly Mock<IProductRepository> _repository;

        public ProductRepositoryBuilder() => _repository = new Mock<IProductRepository>();
        //public IProductRepository Build() => _repository.Object;

        public void ExistsByNameAsync(string name)
        {
            _repository.Setup(repository => repository.ExistsByNameAsync(name)).ReturnsAsync(true);
        }

        private readonly ConcurrentDictionary<Guid, Product> _products = new();

        public ProductRepositoryBuilder WithProduct(Guid id, string? name = null, decimal? price = null)
        {
            var product = new Product(name ?? $"Test Product {id.ToString()[..8]}", price ?? 100m);
            product.ID = id;

            _products[id] = product;
            return this;
        }

        public ProductRepositoryBuilder WithProductsFromIds(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
                WithProduct(id);
            return this;
        }

        public IProductRepository Build()
        {
            _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    _products.TryGetValue(id, out var p);
                    return p;
                });

            return _repository.Object;
        }
    }
}
