using AquaticFishECommerce.Application.DTOs.CartItem;
using AquaticFishECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return Guid.Parse(userId);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();

            var cart = await _cartService.GetCartAsync(userId);

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = GetUserId();

            await _cartService.AddToCartAsyn(userId, dto);

            return Ok(new
            {
                Message = "Product added to cart successfully."
            });
        }

        [HttpPut("{cartItemId:guid}")]
        public async Task<IActionResult> UpdateQuantity(
            Guid cartItemId,
            UpdateCartItemDto dto)
        {
            var userId = GetUserId();

            await _cartService.UpdateQuantityAsync(userId, cartItemId, dto);

            return Ok(new
            {
                Message = "Cart updated successfully."
            });
        }

        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> RemoveItem(Guid cartItemId)
        {
            var userId = GetUserId();

            await _cartService.RemoveItemAsync(userId, cartItemId);

            return Ok(new
            {
                Message = "Item removed successfully."
            });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            await _cartService.ClearCartAsync(userId);

            return Ok(new
            {
                Message = "Cart cleared successfully."
            });
        }
    }
}