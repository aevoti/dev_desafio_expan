using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using DesafioAEVO.Application.UseCases.Product;
using DesafioAEVO.Exceptions.ExceptionsBase;
using DesafioAEVO.Exceptions.Resources;
using FluentAssertions;

namespace UseCases.Test.Product
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestCreateProductJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.ExecuteAsync(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Product_Already_Registered()
        {
            var request = RequestCreateProductJsonBuilder.Build();

            var useCase = CreateUseCase(request.Name);

            Func<Task> action = async () => await useCase.ExecuteAsync(request);

            (await action.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1
                && e.ErrorMessages.Contains(ResourceExceptions.DUPLICATE_PRODUCT_NAME));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestCreateProductJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> action = async () => await useCase.ExecuteAsync(request);

            (await action.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1
                && e.ErrorMessages.Contains(ResourceExceptions.PRODUCT_NAME_EMPTY));
        }

        private CreateProductUseCase CreateUseCase(string? name = null)
        {
            var mapper = MapperBuilder.Build();
            var productRepositoryBuilder = new ProductRepositoryBuilder();
            var unityOfWork = UnitOfWorkBuilder.Build();

            if (string.IsNullOrEmpty(name) == false)
            {
                productRepositoryBuilder.ExistsByNameAsync(name);
            }

            return new CreateProductUseCase(productRepositoryBuilder.Build(), mapper, unityOfWork);
        }
    }
}
