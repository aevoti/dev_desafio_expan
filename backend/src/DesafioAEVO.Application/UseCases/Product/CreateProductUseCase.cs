using AutoMapper;
using DesafioAEVO.Application.UseCases.Product.Validators;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;
using DesafioAEVO.Domain.Abstractions.Repositories;
using DesafioAEVO.Exceptions.ExceptionsBase;
using DesafioAEVO.Exceptions.Resources;

namespace DesafioAEVO.Application.UseCases.Product
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductUseCase(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateProductResponse> ExecuteAsync(RequestProductJson request)
        {
            await ValidateRequestAsync(request);

            var product = new Domain.Entities.Product(request.Name, request.Price);

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesOnDBAsync();

            return new CreateProductResponse
            {
                Name = request.Name,
                Price = request.Price
            };
        }

        private async Task ValidateRequestAsync(RequestProductJson request)
        {
            var validator = new NewProductValidator();
            var validationResult = validator.Validate(request);

            var exists = await _productRepository.ExistsByNameAsync(request.Name);
            if (exists)
            {
                validationResult.Errors.Add(new FluentValidation.Results
                    .ValidationFailure(string.Empty, ResourceExceptions.DUPLICATE_PRODUCT_NAME));
            }

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }
}
