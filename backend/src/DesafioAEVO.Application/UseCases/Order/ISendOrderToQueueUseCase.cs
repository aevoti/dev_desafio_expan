using DesafioAEVO.Communication.Events;

namespace DesafioAEVO.Application.UseCases.Order
{
    public interface ISendOrderToQueueUseCase
    {
        public Task ExecuteAsync(OrderCreatedEvent @event);
    }
}
