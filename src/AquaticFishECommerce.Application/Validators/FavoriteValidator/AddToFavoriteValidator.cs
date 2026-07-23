using AquaticFishECommerce.Application.DTOs.Favorite;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.FavoriteValidator
{
    internal class AddToFavoriteValidator : AbstractValidator<AddFavoriteDto>
    {
        public AddToFavoriteValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required.");
        }
    }
}
