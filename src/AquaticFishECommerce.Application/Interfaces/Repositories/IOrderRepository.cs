using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrderByUserIdAsync(Guid userId);
        Task<Order?> GetOrderWithItemsAsync(Guid orderId);
        Task<bool> HasOrdersWithAddressAsync(Guid addressId);
    }

}
