using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Category;
using AquaticFishECommerce.Application.Interfaces.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AquaticFishECommerce.Application.Interfaces.Services;
using FluentValidation;
using AquaticFishECommerce.Application.Validators.CategoryValidator;

namespace AquaticFishECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        public CategoriesController(ICategoryService categoryService , IValidator<CreateCategoryDto> createCategoryValidator , IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryService = categoryService;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        [HttpGet]
        [AllowAnonymous]
        //Controller for get all category
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<CategoryResponseDto>>
            {
                Success = true,
                Message = "Categories fetched successfully.",
                Data = categories
            });
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        //Controller for get one category by id
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(new ApiResponse<CategoryResponseDto>
            {
                Success = true,
                Message = "Category fetched successfully.",
                Data = category
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        //Controller for create category
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var validator = await _createCategoryValidator.ValidateAsync(dto);
            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }


            var category = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                new ApiResponse<CategoryResponseDto>
                {
                    Success = true,
                    Message = "Category created successfully.",
                    Data = category
                });
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        //Controller for update category
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
        {
            var validator = await _updateCategoryValidator.ValidateAsync(dto);
            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }

            await _categoryService.UpdateAsync(id, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category updated successfully."
            });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        //Controller for delete category
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category deleted successfully."
            });
        }
    }
}