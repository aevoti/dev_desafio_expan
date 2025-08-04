using OrderApi.Models;
using System.Threading.Tasks;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderRequest request);
    }
}
