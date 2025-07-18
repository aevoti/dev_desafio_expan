using DesafioAEVO.Domain.Entities;

namespace DesafioAEVO.Domain.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        public Task AddAsync(Product product);
        Task<bool> ExistsByNameAsync(string name);
        Task<Product> GetByIdAsync(Guid id);
    }
}
