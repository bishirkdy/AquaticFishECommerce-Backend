using AquaticFishECommerce.Application.DTOs.Order;


namespace AquaticFishECommerce.Application.Interfaces.Services.Order
{
    public interface IAdminOrderService
    {
        Task<List<OrderResponseDto>> GetAllOrderAsync();

    }
}
