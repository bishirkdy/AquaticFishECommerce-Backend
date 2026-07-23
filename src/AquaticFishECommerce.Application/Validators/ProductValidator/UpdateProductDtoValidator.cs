using AquaticFishECommerce.Application.DTOs.Product;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.ProductValidator
{
    public class UpdateProductDtoValidator : ProductDtoValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}