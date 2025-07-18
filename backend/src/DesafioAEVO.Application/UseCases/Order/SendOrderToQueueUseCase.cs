using DesafioAEVO.Application.Abstractions;
using DesafioAEVO.Communication.Events;

namespace DesafioAEVO.Application.UseCases.Order
{
    public class SendOrderToQueueUseCase : ISendOrderToQueueUseCase
    {
        private readonly IEventPublisher _eventPublisher;

        public SendOrderToQueueUseCase(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task ExecuteAsync(OrderCreatedEvent @event)
        {
            return _eventPublisher.PublishAsync(@event);
        }
    }
}