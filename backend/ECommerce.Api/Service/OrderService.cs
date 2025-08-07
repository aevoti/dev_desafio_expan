using System.Threading.Tasks;
using ECommerce.Domain;
using ECommerce.Application;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
        {
            // instância de Order com base no request recebido
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Pending,
                Items = request.Items,
                CreatedAt = DateTime.UtcNow
            };

            return await Task.FromResult(order);
        }
    }
}
