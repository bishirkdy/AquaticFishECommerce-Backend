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
        //Controller to add product for admin
        public async Task<IActionResult> Create(CreateProductDto dto)
            {
                var product = await _productService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.Id },
                    new ApiResponse<ProductResponseDto>
                    {
                        Success = true,
                        Message = $"{product.Name} Created Successfully",
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
