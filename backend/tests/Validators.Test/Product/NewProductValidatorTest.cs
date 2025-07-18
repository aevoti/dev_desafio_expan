using CommonTestUtilities.Requests;
using DesafioAEVO.Application.UseCases.Product.Validators;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Exceptions.Resources;
using FluentAssertions;

namespace Validators.Test.Product
{
    public class NewProductValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new NewProductValidator();
            var request = RequestCreateProductJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new NewProductValidator();
            var request = RequestCreateProductJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage == ResourceExceptions.PRODUCT_NAME_EMPTY);
        }

        [Fact]
        public void Should_Return_Error_When_Name_Is_Too_Long()
        {
            var validator = new NewProductValidator();
            var request = new RequestProductJson
            {
                Name = new string('A', 256),
                Price = 100
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage == ResourceExceptions.PRODUCT_NAME_TOO_LONG);
        }
    }
}
