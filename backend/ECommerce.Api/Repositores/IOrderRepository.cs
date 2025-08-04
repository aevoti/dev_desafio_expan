using OrderApi.Models;
using System;
using System.Threading.Tasks;

namespace OrderApi.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateStatusAsync(Guid orderId, OrderStatus status);
        Task<Order> GetByIdAsync(Guid orderId);
    }
}
