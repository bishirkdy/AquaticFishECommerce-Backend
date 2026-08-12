using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.Common.Responses;
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

        //Update order status
        public async Task UpdateOrderStatusAsync(Guid orderId, Guid productId, UpdateOrderStatusDto dto)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found.");

            var item = order.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item == null)
                throw new NotFoundException("Product not found.");

            if (item.OrderStatus == OrderStatus.Delivered)
                throw new BadRequestException("Delivered order cannot be updated.");

            if (item.OrderStatus == OrderStatus.Cancelled)
                throw new BadRequestException("Cancelled order cannot be updated.");

            if (item.OrderStatus == dto.Status)
                throw new BadRequestException("Order is already in this status.");

            switch (dto.Status)
            {
                case OrderStatus.Confirmed:
                    item.ConfirmedAt ??= DateTime.UtcNow;
                    break;

                case OrderStatus.Packed:
                    item.PackedAt ??= DateTime.UtcNow;
                    break;

                case OrderStatus.Shipping:
                    item.ShippingAt ??= DateTime.UtcNow;
                    break;

                case OrderStatus.Shipped:
                    item.OutForDeliveryAt ??= DateTime.UtcNow;
                    break;

                case OrderStatus.Delivered:
                    item.DeliveredAt ??= DateTime.UtcNow;
                    break;

                case OrderStatus.Cancelled:
                    item.CancelledAt ??= DateTime.UtcNow;
                    break;
            }

            item.OrderStatus = dto.Status;

            await _orderRepository.UpdateAsync(order);
        }

        //Delete Order of user
        public async Task DeleteOrderOfUser(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            await _orderRepository.DeleteAsync(order);
        }

        //Service for all order for admin by query
        public async Task<PaginatedResponse<OrderResponseDto>> GetOrdersAsync(OrderPaginatedQueryDto request)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0
                ? 5
                : Math.Min(request.PageSize, 100);

            var (orders, totalCount) =
                await _orderRepository.GetOrdersAsync(
                    page,
                    pageSize,
                    request.Search,
                    request.Status);

            var data = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);

            return new PaginatedResponse<OrderResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
