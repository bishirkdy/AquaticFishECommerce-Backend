using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.CartItem;
using AquaticFishECommerce.Application.Interfaces.Services.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) 
        {
            _cartService = cartService;
        }



        //Controller for get Cart item of user
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync(UserId);
            return Ok(new ApiResponse<CartResponseDto>
            {
                Success = true,
                Message = "Cart item fetched successfully",
                Data = cart
            });
        }

        //Controller for add to cart of user
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            Console.WriteLine(dto);
            await _cartService.AddToCartAsyn(UserId, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product added to cart successfully."
            });
        }

        //Controller for update quantity of user
        [HttpPatch("{cartItemId:guid}")]
        public async Task<IActionResult> UpdateQuantity(
            Guid cartItemId,
            UpdateCartItemDto dto)
        {

            await _cartService.UpdateQuantityAsync(UserId, cartItemId, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cart updated successfully."
            });
        }

        //Controller for delete cart item of user
        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> RemoveItem(Guid cartItemId)
        {

            await _cartService.RemoveItemAsync(UserId, cartItemId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Item removed successfully."
            });
        }

        //Controller for clear cart of user
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync(UserId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cart cleared successfully."
            });
        }
    }
}