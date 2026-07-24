using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AquaticFishECommerce.Application.Interfaces.Services;


namespace AquaticFishECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService )
        {
            _categoryService = categoryService;
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