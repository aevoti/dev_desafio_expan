using MassTransit;
using Microsoft.Extensions.Logging;
using OrderApi.Messages;
using OrderApi.Repositories;
using System;
using System.Threading.Tasks;

namespace OrderApi.Consumers
{
    public class OrderConsumer : IConsumer<OrderCreatedMessage>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(IOrderRepository orderRepository, ILogger<OrderConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            var orderId = context.Message.OrderId;
            _logger.LogInformation($"Iniciando processamento do pedido {orderId}");

            // Atualiza para Em Processamento após 30s
            await Task.Delay(TimeSpan.FromSeconds(30));
            await _orderRepository.UpdateStatusAsync(orderId, OrderStatus.Processing);

            // Aguarda mais 90s e decide se conclui ou falha
            await Task.Delay(TimeSpan.FromSeconds(90));
            var random = new Random();
            var success = random.NextDouble() >= 0.5;

            var finalStatus = success ? OrderStatus.Completed : OrderStatus.Failed;
            await _orderRepository.UpdateStatusAsync(orderId, finalStatus);

            _logger.LogInformation($"Pedido {orderId} finalizado com status: {finalStatus}");
        }
    }
}
