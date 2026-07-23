using AquaticFishECommerce.API.Requests.Product;
using FluentValidation;

namespace AquaticFishECommerce.API.Validator.Product
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(p => p.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Category is required.");
        }
    }
}
