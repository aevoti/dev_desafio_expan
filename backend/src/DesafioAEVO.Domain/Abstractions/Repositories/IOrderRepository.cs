using DesafioAEVO.Domain.Entities;

namespace DesafioAEVO.Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllWithItemsAsync();
        Task<Order> GetByIdAsync(Guid id);
        public Task AddAsync(Order order);
    }
}
