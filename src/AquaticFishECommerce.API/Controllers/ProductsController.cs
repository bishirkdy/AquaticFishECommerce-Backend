using AquaticFishECommerce.API.Requests.Product;
using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers
{

        [Route("api/v1/[controller]")]
        [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;
            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            [HttpGet]
            [AllowAnonymous]
        //Controller to get all products
        public async Task<IActionResult> GetAll()
            {
                var products = await _productService.GetAllAsync();

                return Ok(new ApiResponse<IEnumerable<ProductResponseDto>>
                {
                    Success = true,
                    Message = "Product fetched successfully",
                    Data = products
                });
            }

            [HttpGet("{id:guid}")]
            [AllowAnonymous]
        //Controller to get only one product by id
        public async Task<IActionResult> GetById(Guid id)
            {
                var product = await _productService.GetByIdAsync(id);

                return Ok(new ApiResponse<ProductResponseDto>
                {
                    Success = true,
                    Message = "Product fetched successfully",
                    Data = product
                });
            }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            // Step 1: Convert Request to DTO
            var dto = new CreateProductDto
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                DiscountPercentage = request.DiscountPercentage,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };

            // Step 2: Open image stream
            Stream? stream = null;
            string? fileName = null;

            if (request.Image != null)
            {
                stream = request.Image.OpenReadStream();
                fileName = request.Image.FileName;
            }

            // Step 3: Call service
            var product = await _productService.CreateAsync(
                dto,
                stream,
                fileName,
                request.IsPrimary);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                new ApiResponse<ProductResponseDto>
                {
                    Success = true,
                    Message = $"{product.Name} created successfully.",
                    Data = product
                });
        }

        [HttpPut("{id:guid}")]
            [Authorize(Roles = "Admin")]
        //Controller to update products for admin
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
            {
            await _productService.UpdateAsync(id, dto);
            return Ok(new ApiResponse
            {
                Success = true,
                Message= "Product updated successfully",
            });
            }

            [HttpDelete("{id:guid}")]
            [Authorize(Roles = "Admin")]
        //Controller to delete products for admin
        public async Task<IActionResult> Delete(Guid id)
            {
                await _productService.DeleteAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product deleted successfully",
                });
            }
        }
}
