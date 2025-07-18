using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;

namespace DesafioAEVO.Application.UseCases.Product
{
    public interface ICreateProductUseCase
    {
        public Task<CreateProductResponse> ExecuteAsync(RequestProductJson request);
    }
}
