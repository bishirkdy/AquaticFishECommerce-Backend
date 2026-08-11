using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // Create Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {

            var orderId = await _orderService.CreateOrderAsync(UserId, dto);

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "Order placed successfully.",
                Data = orderId
            });
        }

        // Get Orders of user
        [HttpGet("me")]
        public async Task<IActionResult> GetMyOrders()
        {

            var orders = await _orderService.GetMyOrdersAsync(UserId);

            return Ok(new ApiResponse<List<OrderResponseDto>>
            {
                Success = true,
                Message = "Orders retrieved successfully.",
                Data = orders
            });
        }

        // Cancel Order Item
        [HttpPatch("{orderId:guid}/cancel/{productId:guid}")]
        public async Task<IActionResult> CancelOrderItem(Guid orderId, Guid productId)
        {
            await _orderService.CancelOrderItemAsync(UserId, productId, orderId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Order item cancelled successfully."
            });
        }
    }
}