using DesafioAEVO.Communication.Responses;
using DesafioAEVO.Domain.Abstractions.Repositories;
using DesafioAEVO.Exceptions.Resources;
using SendGrid.Helpers.Errors.Model;

namespace DesafioAEVO.Application.UseCases.Product
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsUseCase(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponse>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllAsync();

            if (!products.Any() || products == null)
                throw new NotFoundException(ResourceExceptions.PRODUCT_NOT_FOUND);

            return products.Select(p => new ProductResponse
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }
    }
}
