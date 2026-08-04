using AquaticFishECommerce.API.Requests.Product;
using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/product")]
    [Authorize(Roles = "Admin")]
    public class AdminProductController : ControllerBase
    {
        private readonly IAdminProductService _adminProductService;

        public AdminProductController(IAdminProductService adminProductService )
        {
            _adminProductService = adminProductService;

        }



        //Controller to create product with image
        [HttpPost]
        //[FromForm] is used when the client sends data as form data instead of JSON.
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            //Convert Request to DTO
            var dto = new CreateProductDto
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CostPrice = request.CostPrice,
                Stock = request.Stock,
                DiscountPercentage = request.DiscountPercentage,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };

            Stream? stream = null;
            string? fileName = null;

            if (request.Image != null)
            {
                //OpenReadStream() opens the uploaded file for reading and returns a Stream
                stream = request.Image.OpenReadStream();
                fileName = request.Image.FileName;
            }

            // Call service
            var product = await _adminProductService.CreateAsync(
                dto,
                stream,
                fileName,
                request.IsPrimary);

            return Created("" ,
                new ApiResponse<ProductResponseDto>
                {
                    Success = true,
                    Message = $"{product.Name} created successfully.",
                    Data = product
                });


        }

        //Controller to update products for admin
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromForm] UpdateProductRequest request)
        {
            var dto = new UpdateProductDto
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CostPrice = request.CostPrice,
                Stock = request.Stock,
                DiscountPercentage = request.DiscountPercentage,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };

            Stream? stream = null;
            string? fileName = null;

            if (request.Image != null)
            {
                stream = request.Image.OpenReadStream();
                fileName = request.Image.FileName;
            }

            await _adminProductService.UpdateAsync(
                id,
                dto,
                stream,
                fileName,
                request.IsPrimary);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product updated successfully."
            });
        }

        //Controller to delete products for admin
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _adminProductService.DeleteAsync(id);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully",
            });
        }
    }
}
