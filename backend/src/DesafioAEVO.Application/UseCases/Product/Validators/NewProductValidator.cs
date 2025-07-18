using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Exceptions.Resources;
using FluentValidation;

namespace DesafioAEVO.Application.UseCases.Product.Validators
{
    public class NewProductValidator : AbstractValidator<RequestProductJson>
    {
        public NewProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(ResourceExceptions.PRODUCT_NAME_EMPTY);
            RuleFor(p => p.Price).GreaterThan(0).WithMessage(ResourceExceptions.PRICE_ZERO);
            When(p => string.IsNullOrEmpty(p.Name) == false, () =>
            {
                RuleFor(p => p.Name).MaximumLength(255).WithMessage(ResourceExceptions.PRODUCT_NAME_TOO_LONG);
            });
        }
    }
}
