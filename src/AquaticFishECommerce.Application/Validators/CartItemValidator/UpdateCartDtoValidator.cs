using AquaticFishECommerce.Application.DTOs.CartItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.CartItemValidator
{
    public class UpdateCartDtoValidator : AbstractValidator<UpdateCartItemDto>
    {
        public UpdateCartDtoValidator()
        {
            RuleFor(x => x.Quantity)
              .NotEmpty()
              .WithMessage("Quantity is required.")
              .GreaterThan(0)
              .WithMessage("Quantity must be greater than 0.")
              .LessThanOrEqualTo(100)
              .WithMessage("Quantity cannot exceed 100.");
        }
    }
}
