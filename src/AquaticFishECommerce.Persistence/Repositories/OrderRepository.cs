using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) {}

        public async Task<List<Order>> GetOrderByUserIdAsync(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId)
                .Include(o => o.Address)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }


    }
}
