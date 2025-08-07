  
using System.Threading.Tasks;
using ECommerce.Domain;
using ECommerce.Application;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderRequest request);
    }
}
