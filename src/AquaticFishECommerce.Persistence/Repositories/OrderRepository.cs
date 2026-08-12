using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Domain.Enums;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) {}

        //Repository for get orders of user
        public async Task<List<Order>> GetOrderByUserIdAsync(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId)
                .Include(o => o.Address)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        //Repository for get one order with items
        public async Task<Order?> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _dbSet
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> HasOrdersWithAddressAsync(Guid addressId)
        {
            return await _dbSet.AnyAsync(o => o.AddressId == addressId);
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        
        public async Task<bool> HasOrdersAsync(Guid productId)
        {
            return await _context.OrderItems
                .AnyAsync(x => x.ProductId == productId);
        }

        public async Task<bool> HasOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders.AnyAsync(o => o.UserId == userId);
        }

        public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetOrdersAsync(int page, int pageSize, string? search, string? status)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.Address)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Images)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(o =>
                    o.Id.ToString().Contains(search) ||
                    o.PaymentMethod.ToString().Contains(search) ||
                    o.PaymentStatus.ToString().Contains(search) ||
                    o.Address.FullName.Contains(search) ||
                    o.Address.PhoneNumber.Contains(search)
                );
            }

            // Status
            if (!string.IsNullOrWhiteSpace(status) && status != "All")
            {
                if (Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
                {
                    query = query.Where(o =>
                        o.Items.Any(i => i.OrderStatus == orderStatus));
                }
            }

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalCount);
        }
    }
}
