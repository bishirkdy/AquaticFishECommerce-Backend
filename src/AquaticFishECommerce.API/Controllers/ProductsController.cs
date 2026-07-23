using AquaticFishECommerce.API.Requests.Product;
using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers
{

        [Route("api/v1/[controller]")]
        [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;
            //private readonly IValidator<CreateProductDto> _createProductValidator;
            //private readonly IValidator<UpdateProductDto> _updateProductValidator;
            public ProductsController(IProductService productService  
                //, IValidator<CreateProductDto> createProductValidator , IValidator<UpdateProductDto> updateProductValidator
                )
            {
            _productService = productService;
            //_createProductValidator = createProductValidator;
            //_updateProductValidator = updateProductValidator;
            }

        //Controller to get all products
        [HttpGet]
        [AllowAnonymous]
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

        //Controller to get only one product by id
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
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

        //Controller to create product with image
        [HttpPost]
        [Authorize(Roles = "Admin")]
        //[FromForm] is used when the client sends data as form data instead of JSON.
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            //Convert Request to DTO
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
            //var validator = await _createProductValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //{
            //    return BadRequest(new
            //    {
            //        Success = false,
            //        Message = "Validation failed.",
            //        Errors = validator.Errors.Select(e => e.ErrorMessage).ToList()
            //    });
            //}

            //Open image stream
            Stream? stream = null;
            string? fileName = null;

            if (request.Image != null)
            {
                //OpenReadStream() opens the uploaded file for reading and returns a Stream
                stream = request.Image.OpenReadStream();
                fileName = request.Image.FileName;
            }

            // Call service
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

        //Controller to update products for admin
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
            {
            //var validator = await _updateProductValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //{
            //    BadRequest(new
            //    {
            //        Success = false,
            //        Message = $"{dto.Name} created successfully.",
            //        Errors = validator.Errors.Select(e => e.ErrorMessage).ToList()
            //    });
            //}

            await _productService.UpdateAsync(id, dto);
            return Ok(new ApiResponse
            {
                Success = true,
                Message= "Product updated successfully",
            });
            }

        //Controller to delete products for admin
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
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
