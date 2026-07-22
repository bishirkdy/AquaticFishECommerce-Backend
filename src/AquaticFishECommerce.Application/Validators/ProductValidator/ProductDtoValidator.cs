using AquaticFishECommerce.Application.DTOs.Product;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.ProductValidator
{
    public class ProductDtoValidator<T> : AbstractValidator<T> where T : IProductDto 
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.CategoryId)
                .NotEmpty();
        }
    }
}
