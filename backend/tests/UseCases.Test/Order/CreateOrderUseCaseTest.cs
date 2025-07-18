using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using DesafioAEVO.Application.UseCases.Order;
using DesafioAEVO.Communication.Events;
using FluentAssertions;
using Moq;

namespace UseCases.Test.Order
{
    public class CreateOrderUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestCreateOrderJsonBuilder.Build();
            var productIds = request.Items!.Select(i => i.ProductID).ToList();

            var (useCase, sendOrderToQueueMock) = CreateUseCase(productIds);

            var result = await useCase.ExecuteAsync(request);

            result.Should().NotBeNull();
            result.OrderID.Should().NotBe(Guid.Empty);
            result.Status.Should().Be("Received");

            sendOrderToQueueMock.Verify(
                m => m.ExecuteAsync(It.IsAny<OrderCreatedEvent>()),
                Times.Once);
        }

        private (CreateOrderUseCase useCase, Mock<ISendOrderToQueueUseCase> mock) CreateUseCase(List<Guid> productIds)
        {
            var mapper = MapperBuilder.Build();
            var productRepositoryBuilder = new ProductRepositoryBuilder()
                .WithProductsFromIds(productIds);
            var orderRepositoryBuilder = new OrderRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();

            var sendOrderToQueueMock = new Mock<ISendOrderToQueueUseCase>();
            sendOrderToQueueMock.Setup(m => m.ExecuteAsync(It.IsAny<OrderCreatedEvent>()))
                .Returns(Task.CompletedTask);

            var useCase = new CreateOrderUseCase(
                productRepositoryBuilder.Build(),
                orderRepositoryBuilder.Build(),
                mapper,
                unitOfWork,
                sendOrderToQueueMock.Object);

            return (useCase, sendOrderToQueueMock);
        }
    }
}
