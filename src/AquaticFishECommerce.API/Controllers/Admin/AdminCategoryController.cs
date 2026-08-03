using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AquaticFishECommerce.Application.Interfaces.Services.Category;
using AquaticFishECommerce.Application.Interfaces.Services.CategoryService;


namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/category")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IAdminCategoryService _adminCategoryService;
        public CategoriesController(ICategoryService categoryService , IAdminCategoryService adminCategoryService)
        {
            _categoryService = categoryService;
            _adminCategoryService = adminCategoryService;
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
        //Controller for create category
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var category = await _adminCategoryService.CreateAsync(dto);

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
        //Controller for update category
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
        {
            await _adminCategoryService.UpdateAsync(id, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category updated successfully."
            });
        }

        [HttpDelete("{id:guid}")]
        //Controller for delete category
        public async Task<IActionResult> Delete(Guid id)
        {
            await _adminCategoryService.DeleteAsync(id);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category deleted successfully."
            });
        }
    }
}