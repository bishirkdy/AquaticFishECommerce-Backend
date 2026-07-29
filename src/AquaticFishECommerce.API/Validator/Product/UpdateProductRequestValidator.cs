using AquaticFishECommerce.API.Requests.Product;
using FluentValidation;

namespace AquaticFishECommerce.API.Validator.Product
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(100)
                .When(p => !string.IsNullOrWhiteSpace(p.Name))
                .WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(1000)
                .When(p => !string.IsNullOrWhiteSpace(p.Description))
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .When(p => p.Price.HasValue)
                .WithMessage("Price must be greater than zero.");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                .When(p => p.Stock.HasValue)
                .WithMessage("Stock cannot be negative.");

            RuleFor(p => p.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .When(p => p.DiscountPercentage.HasValue)
                .WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .When(p => p.CategoryId.HasValue)
                .WithMessage("Category is invalid.");
        }
    }
}