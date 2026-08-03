using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using AquaticFishECommerce.Infrastructure.Services.Business.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/order")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IAdminOrderService _adminOrderService;

        public AdminOrderController(IAdminOrderService adminOrderService)
        {
            _adminOrderService = adminOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _adminOrderService.GetAllOrderAsync();

            return Ok(new ApiResponse<List<OrderResponseDto>>
            {
                Success = true,
                Message = "All Order Fetched Successfully",
                Data = orders
            });
        }

        [HttpPatch("{orderId}/products/{productId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, Guid productId, UpdateOrderStatusDto dto)
        {
            await _adminOrderService.UpdateOrderStatusAsync(orderId, productId, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Order Updated Successfully"
            });
        }

        [HttpDelete("{orderId:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _adminOrderService.DeleteOrderOfUser(orderId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Order deleted successfully."
            });
        }
    }
}