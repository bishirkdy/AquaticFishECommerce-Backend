using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Services;
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
            public async Task<IActionResult> GetAll()
            {
                var products = await _productService.GetAllAsync();

                return Ok(products);
            }

            [HttpGet("{id:guid}")]
            [AllowAnonymous]
            public async Task<IActionResult> GetById(Guid id)
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                    return NotFound(new
                    {
                        Message = "Product not found."
                    });

                return Ok(product);
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Create(CreateProductDto dto)
            {
                var product = await _productService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.Id },
                    product);
            }

            [HttpPut("{id:guid}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
            {
                await _productService.UpdateAsync(id, dto);

                return NoContent();
            }

            [HttpDelete("{id:guid}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Delete(Guid id)
            {
                await _productService.DeleteAsync(id);

                return NoContent();
            }
        }
}
