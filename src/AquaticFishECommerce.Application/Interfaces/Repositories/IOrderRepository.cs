using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrderByUserIdAsync(Guid userId);
        Task<Order?> GetOrderWithItemsAsync(Guid orderId);
        Task<bool> HasOrdersWithAddressAsync(Guid addressId);
        Task<List<Order>> GetAllOrderAsync();
        Task<bool> HasOrdersAsync(Guid productId);
        Task<bool> HasOrdersByUserIdAsync(Guid userId);
    }

}
