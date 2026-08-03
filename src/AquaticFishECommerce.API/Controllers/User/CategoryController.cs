using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AquaticFishECommerce.Application.Interfaces.Services.Category;


namespace AquaticFishECommerce.API.Controllers.User
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
    }
}