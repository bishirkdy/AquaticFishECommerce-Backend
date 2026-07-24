using AquaticFishECommerce.Application.DTOs.Product;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.ProductValidator
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Product name cannot exceed 100 characters.")
                .When(x => x.Name is not null);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.")
                .When(x => x.Description is not null);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.")
                .When(x => x.Stock.HasValue);

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Discount percentage must be between 0 and 100.")
                .When(x => x.DiscountPercentage.HasValue);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required.")
                .When(x => x.CategoryId.HasValue);

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("Product status is required.")
                .When(x => x.IsActive.HasValue);
        }
    }
}