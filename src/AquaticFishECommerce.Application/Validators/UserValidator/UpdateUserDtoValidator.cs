using AquaticFishECommerce.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.UserValidator
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must matches to exact 10 number");
        }
    }
}
