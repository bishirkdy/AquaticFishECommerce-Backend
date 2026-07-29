using AquaticFishECommerce.API.Requests.Product;
using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.User
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

        ////Controller to get all products
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAll()
        //    {
        //        var products = await _productService.GetAllAsync();

        //        return Ok(new ApiResponse<IEnumerable<ProductResponseDto>>
        //        {
        //            Success = true,
        //            Message = "Product fetched successfully",
        //            Data = products
        //        });
        //    }

        //Controller for get queriable image
        [HttpGet]

        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDto query)
        {
            var products = await _productService.GetQuariableAsync(query);

            return Ok(new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Success = true,
                Message = "Products fetched successfully.",
                Data = products
            });
        }

        //Controller for get six products
        [HttpGet("six-product")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSixProduct()
        {
            var product = await _productService.GetSixAsync();
            return Ok(new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Success = true,
                Message = "Product featched successfully",
                Data = product
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
        }
}
