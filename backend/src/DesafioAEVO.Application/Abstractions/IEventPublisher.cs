namespace DesafioAEVO.Application.Abstractions
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
