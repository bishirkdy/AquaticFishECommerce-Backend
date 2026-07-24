using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("Invalid user.");

            return Guid.Parse(userId);
        }

        // Create Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var userId = GetUserId();

            var orderId = await _orderService.CreateOrderAsync(userId, dto);

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "Order placed successfully.",
                Data = orderId
            });
        }

        // Get Orders of user
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();

            var orders = await _orderService.GetMyOrdersAsync(userId);

            return Ok(new ApiResponse<List<OrderResponseDto>>
            {
                Success = true,
                Message = "Orders retrieved successfully.",
                Data = orders
            });
        }

        // Cancel Order Item
        [HttpPut("{orderId:guid}/cancel/{productId:guid}")]
        public async Task<IActionResult> CancelOrderItem(Guid orderId, Guid productId)
        {
            var userId = GetUserId();

            await _orderService.CancelOrderItemAsync(userId, productId, orderId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Order item cancelled successfully."
            });
        }
    }
}