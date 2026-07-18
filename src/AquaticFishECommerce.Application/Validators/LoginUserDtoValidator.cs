using AquaticFishECommerce.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators
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
