using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Order;


namespace AquaticFishECommerce.Application.Interfaces.Services.Order
{
    public interface IAdminOrderService
    {
        Task<List<OrderResponseDto>> GetAllOrderAsync();
        Task UpdateOrderStatusAsync(Guid orderId, Guid productId, UpdateOrderStatusDto dto);
        Task DeleteOrderOfUser(Guid orderId);
        Task<PaginatedResponse<OrderResponseDto>> GetOrdersAsync(OrderPaginatedQueryDto request);
    }
}
