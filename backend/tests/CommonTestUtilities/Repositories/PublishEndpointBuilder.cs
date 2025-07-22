using MassTransit;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class PublishEndpointBuilder
    {
        public static Mock<IPublishEndpoint> Build(out IPublishEndpoint endpoint)
        {
            var mock = new Mock<IPublishEndpoint>();
            mock.Setup(p => p.Publish(
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            endpoint = mock.Object;
            return mock;
        }
    }
}
