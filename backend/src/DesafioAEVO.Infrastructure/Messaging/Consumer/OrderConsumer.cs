using DesafioAEVO.Communication.Events;
using DesafioAEVO.Domain.Abstractions.Repositories;
using MassTransit;

namespace DesafioAEVO.Infrastructure.Messaging.Consumer
{
    public class OrderConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderConsumer(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            var order = await _orderRepository.GetByIdAsync(message.OrderId);
            if (order == null) return;

            await Task.Delay(TimeSpan.FromSeconds(30));
            order.UpdateStatus(Domain.Enums.OrderStatus.Processing);
            await _unitOfWork.SaveChangesOnDBAsync();

            await Task.Delay(TimeSpan.FromSeconds(90));
            var random = new Random().Next(0, 2);
            var newStatus = random == 0 ? Domain.Enums.OrderStatus.Failed : Domain.Enums.OrderStatus.Completed;
            order.UpdateStatus(newStatus);
            await _unitOfWork.SaveChangesOnDBAsync();
        }
    }
}
