using AquaticFishECommerce.Application.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Validators.CategoryValidator
{
    public class CategoryDtoValidator<T> : AbstractValidator<T> where T : ICategoryDto
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500);
        }
    }
}
