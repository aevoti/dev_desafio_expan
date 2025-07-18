using DesafioAEVO.Communication.Responses;

namespace DesafioAEVO.Application.UseCases.Order
{
    public interface IGetAllOrdersUseCase
    {
        public Task<IEnumerable<OrderResponse>> ExecuteAsync();
    }
}
