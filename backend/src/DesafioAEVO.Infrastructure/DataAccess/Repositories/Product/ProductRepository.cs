using DesafioAEVO.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioAEVO.Infrastructure.DataAccess.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DesafioAEVOdbContext _dbContext;
        public ProductRepository(DesafioAEVOdbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task AddAsync(Domain.Entities.Product product) => await _dbContext.Products.AddAsync(product);

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbContext.Products.AnyAsync(p => p.Name == name);
        }

        public async Task<Domain.Entities.Product> GetByIdAsync(Guid id) => await _dbContext.Products.FirstOrDefaultAsync(p => p.ID == id);
    }
}
