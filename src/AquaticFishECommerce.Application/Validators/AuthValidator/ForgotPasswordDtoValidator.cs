using AquaticFishECommerce.Application.DTOs.Auth;
using FluentValidation;

namespace AquaticFishECommerce.Application.Validators.AuthValidator
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
        }
    }
}
