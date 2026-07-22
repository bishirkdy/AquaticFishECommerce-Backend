using AquaticFishECommerce.Application.DTOs.ProductImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.ProductImageValidator
{
    public class ProductImageValidator<T> : AbstractValidator<T> where T: CreateProductImageDto
    {
        public ProductImageValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.IsPrimary)
                .NotNull();
        }
    }
}
