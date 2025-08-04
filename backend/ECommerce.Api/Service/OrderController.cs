using MassTransit;
using Microsoft.Extensions.Logging;
using OrderApi.Models;
using OrderApi.Messages;
using OrderApi.Repositories;
using System;
using System.Threading.Tasks;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IPublishEndpoint publishEndpoint,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Received,
                Items = request.Items
            };

            await _orderRepository.AddAsync(order);

            var orderMessage = new OrderCreatedMessage
            {
                OrderId = order.Id,
                Items = order.Items
            };

            await _publishEndpoint.Publish(orderMessage);

            _logger.LogInformation($"Pedido {order.Id} criado e publicado na fila.");

            return order;
        }
    }
}
