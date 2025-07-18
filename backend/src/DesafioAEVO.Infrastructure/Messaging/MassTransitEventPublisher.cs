using DesafioAEVO.Application.Abstractions;
using MassTransit;

namespace DesafioAEVO.Infrastructure.Messaging
{
    public class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) => await _publishEndpoint.Publish(@event);
    }
}
