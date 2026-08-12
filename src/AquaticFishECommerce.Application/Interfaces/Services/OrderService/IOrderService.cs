using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Application.Interfaces.Services.Order
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId, CreateOrderDto dto , PaymentStatus paymentStatus = PaymentStatus.Pending);
        Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId);
        Task CancelOrderItemAsync(Guid userId, Guid orderId, Guid productId);
    }
}
