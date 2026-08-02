using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using AquaticFishECommerce.Domain.Enums;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.OrderServices
{
    public class AdminOrderService : IAdminOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public AdminOrderService(IOrderRepository orderRepository , IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        //Service for get all order for admin
        public async Task<List<OrderResponseDto>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();

            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, Guid productId, UpdateOrderStatusDto dto)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");
            var item = order.Items
                .FirstOrDefault(x => x.ProductId == productId);
            
            if (item == null)
                throw new NotFoundException("Product not found");

            item.OrderStatus = dto.Status;

            switch (dto.Status)
            {
                case OrderStatus.Confirmed:
                    item.ConfirmedAt = DateTime.UtcNow;
                    break;

                case OrderStatus.Packed:
                    item.PackedAt = DateTime.UtcNow;
                    break;

                case OrderStatus.Shipping:
                    item.ShippingAt = DateTime.UtcNow;
                    break;

                case OrderStatus.Shipped:
                    item.OutForDeliveryAt = DateTime.UtcNow;
                    break;

                case OrderStatus.Delivered:
                    item.DeliveredAt = DateTime.UtcNow;
                    break;

                case OrderStatus.Cancelled:
                    item.CancelledAt = DateTime.UtcNow;
                    break;
            }

            await _orderRepository.UpdateAsync(order);
        }
    }
}
