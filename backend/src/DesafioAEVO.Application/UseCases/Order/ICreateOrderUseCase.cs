using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;

namespace DesafioAEVO.Application.UseCases.Order
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderResponse> ExecuteAsync(RequestOrderJson request);

    }
}
