using DesafioAEVO.Domain.Abstractions.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class OrderRepositoryBuilder
    {
        private readonly Mock<IOrderRepository> _repository;

        public OrderRepositoryBuilder() => _repository = new Mock<IOrderRepository>();
        public IOrderRepository Build() => _repository.Object;
    }
}
