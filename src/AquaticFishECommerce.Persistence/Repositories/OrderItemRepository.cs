using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem> ,  IOrderItemsRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context){}
    }
}
