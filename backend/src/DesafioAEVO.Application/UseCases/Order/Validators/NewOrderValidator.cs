using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Exceptions.Resources;
using FluentValidation;

namespace DesafioAEVO.Application.UseCases.Order.Validators
{
    public class NewOrderValidator : AbstractValidator<RequestOrderJson>
    {
        public NewOrderValidator()
        {
            RuleFor(o => o.Items)
                .NotNull().WithMessage(ResourceExceptions.ORDER_ITEMS_REQUIRED)
                .Must(items => items != null && items.Any())
                .WithMessage(ResourceExceptions.ORDER_MUST_HAVE_AT_LEAST_ONE_ITEM);

            RuleForEach(o => o.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage(ResourceExceptions.QUANTITY_MUST_BE_GREATER_THAN_ZERO);
            });
        }
    }
}
