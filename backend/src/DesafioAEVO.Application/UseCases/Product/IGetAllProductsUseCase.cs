using DesafioAEVO.Communication.Responses;

namespace DesafioAEVO.Application.UseCases.Product
{
    public interface IGetAllProductsUseCase
    {
        public Task<IEnumerable<ProductResponse>> ExecuteAsync();
    }
}
