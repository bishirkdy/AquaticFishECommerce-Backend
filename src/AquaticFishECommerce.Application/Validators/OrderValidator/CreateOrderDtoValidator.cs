using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Domain.Enums;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.OrderValidator
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.AddressId)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Invalid payment method.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one order item is required.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateOrderItemDtoValidator());
        }
    }
}