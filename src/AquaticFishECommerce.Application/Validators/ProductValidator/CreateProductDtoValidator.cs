using AquaticFishECommerce.Application.DTOs.Product;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.ProductValidator
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.Stock)
                .NotNull().WithMessage("Product stock is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Product stock cannot be negative.");

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Product category is required.");
        }
    }
}