using DesafioAEVO.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioAEVO.Infrastructure.DataAccess.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DesafioAEVOdbContext _dbContext;
        public OrderRepository(DesafioAEVOdbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Domain.Entities.Order order) => await _dbContext.Orders.AddAsync(order);

        public async Task<Domain.Entities.Order> GetByIdAsync(Guid id) => await _dbContext.Orders.FirstOrDefaultAsync(p => p.ID == id);

        public async Task<IEnumerable<Domain.Entities.Order>> GetAllWithItemsAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();
        }


    }
}
