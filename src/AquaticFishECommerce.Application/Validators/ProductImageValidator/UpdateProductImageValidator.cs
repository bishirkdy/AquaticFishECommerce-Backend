using AquaticFishECommerce.Application.DTOs.ProductImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.ProductImageValidator
{
    public class UpdateProductImageValidator : AbstractValidator<UpdateProductImageDto>
    {
        public UpdateProductImageValidator()
        {
            RuleFor(x => x.IsPrimary)
                .NotNull();
        }
    }
}
