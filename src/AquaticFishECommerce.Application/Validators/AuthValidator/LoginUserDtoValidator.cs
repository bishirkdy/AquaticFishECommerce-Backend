using AquaticFishECommerce.Application.DTOs.Auth;
using FluentValidation;


namespace AquaticFishECommerce.Application.Validators.AuthValidator
{
    public class LoginUserDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

        }
    }
}
