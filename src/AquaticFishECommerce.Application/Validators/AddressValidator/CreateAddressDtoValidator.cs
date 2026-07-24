using AquaticFishECommerce.Application.DTOs.Address;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.AddressValidator
{
    public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$")
                .WithMessage("Phone number must be exactly 10 digits.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

            RuleFor(x => x.Post)
                .NotEmpty().WithMessage("Post is required.")
                .MaximumLength(100).WithMessage("Post cannot exceed 100 characters.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District is required.")
                .MaximumLength(100).WithMessage("District cannot exceed 100 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(100).WithMessage("State cannot exceed 100 characters.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage("Pincode is required.")
                .Matches(@"^\d{6}$")
                .WithMessage("Pincode must be exactly 6 digits.");

            RuleFor(x => x.Landmark)
                .MaximumLength(200)
                .WithMessage("Landmark cannot exceed 200 characters.")
                .When(x => x.Landmark is not null);
        }
    }
}