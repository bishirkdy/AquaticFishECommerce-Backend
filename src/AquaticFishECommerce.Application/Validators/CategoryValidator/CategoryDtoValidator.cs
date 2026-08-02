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
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500);
        }
    }
}
