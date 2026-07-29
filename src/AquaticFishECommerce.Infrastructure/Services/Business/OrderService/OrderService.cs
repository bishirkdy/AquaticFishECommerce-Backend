using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Domain.Enums;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IAddressRepository _addressReporitory;
        private readonly IMapper _mapper;
        
        public OrderService(IUserRepository userRepository , IOrderRepository orderRepository , IProductRepository productRepository , ICartItemRepository cartItemRepository , IAddressRepository addressRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _addressReporitory = addressRepository;
            _mapper = mapper;
        }

        //Service for create order
        public async Task<Guid> CreateOrderAsync(Guid userId, CreateOrderDto dto)
        {
            // Validate User
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User not found.");

            // Validate Address
            var address = await _addressReporitory.GetByIdAsync(dto.AddressId);
            if (address == null)
                throw new NotFoundException("Address not found.");

            if (address.UserId != userId)
                throw new BadRequestException("This address does not belong to the current user.");

            decimal totalAmount = 0;
            decimal totalProfit = 0;

            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new NotFoundException("Product not found.");

                if (!product.IsActive)
                    throw new BadRequestException($"{product.Name} is not available.");

                if (product.Stock < item.Quantity)
                    throw new BadRequestException($"{product.Name} has only {product.Stock} items remaining.");

                // Selling price before discount
                decimal unitPrice = product.Price;

                // Cost price
                decimal costPrice = product.CostPrice;

                // Discount amount
                decimal discountAmount = unitPrice * (product.DiscountPercentage / 100);

                // Selling price after discount
                decimal finalPrice = unitPrice - discountAmount;

                // Total amount of this item
                decimal itemTotal = finalPrice * item.Quantity;

                // Profit of this item
                decimal itemProfit = (finalPrice - costPrice) * item.Quantity;

                totalAmount += itemTotal;
                totalProfit += itemProfit;

                // Reduce stock
                product.Stock -= item.Quantity;
                await _productRepository.UpdateAsync(product);

                // Create Order Item Snapshot
                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,

                    UnitPrice = unitPrice,
                    CostPrice = costPrice,

                    DiscountPercentage = product.DiscountPercentage,

                    TotalPrice = itemTotal,

                    OrderStatus = OrderStatus.OrderPlaced
                });
            }

            var order = new Order
            {
                UserId = userId,
                AddressId = dto.AddressId,
                PaymentMethod = dto.PaymentMethod,

                TotalAmount = totalAmount,
                Profit = totalProfit,

                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.OrderPlaced,

                Items = orderItems
            };
            await _orderRepository.AddAsync(order);
            // Clear Cart
            await _cartItemRepository.ClearCartAsync(userId);
            return order.Id;
        }

        //Service for get all orders of one user
        public async Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrderByUserIdAsync(userId);
            var response = new List<OrderResponseDto>();

            foreach (var order in orders)
            {
                var orderDto = _mapper.Map<OrderResponseDto>(order);
                response.Add(orderDto);
            }

            return response;
        }

        public async Task CancelOrderItemAsync(Guid userId ,Guid productId , Guid orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId); 
            if (order == null)
            {
                throw new NotFoundException("Order Not Fount");
            }

            if(order.UserId != userId)
            {
                throw new UnauthorizedException("UnAuthorized");
            }

            var orderItem = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if(orderItem == null)
            {
                throw new NotFoundException("Order item not fount");
            }
            orderItem.OrderStatus = OrderStatus.Cancelled;
            orderItem.CancelledAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);

        }      
        
    }
}
