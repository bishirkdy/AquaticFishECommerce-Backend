using AquaticFishECommerce.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId, CreateOrderDto dto);
        Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId);
        Task CancelOrderItemAsync(Guid userId, Guid orderId, Guid productId);
    }
}
