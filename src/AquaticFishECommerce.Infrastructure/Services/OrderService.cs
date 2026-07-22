using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Domain.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services
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

        public async Task<Guid> CreateOrderAsync(Guid userId, CreateOrderDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new NotFoundException("User not fount");
            }
            var address = await _addressReporitory.GetByIdAsync(dto.AddressId);
            if(address == null)
            {
                throw new NotFoundException("Address not fount");
            }

            if(address.UserId != userId)
            {
                throw new BadRequestException("This address does not belong to the current user.");
            }

            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();
            foreach(var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new NotFoundException("Product is not fount");

                if (!product.IsActive)
                {
                    throw new NotFoundException("Product is not available");
                }

                if(product.Stock < item.Quantity)
                {
                    throw new Exception($"{product.Name} has only {product.Stock} item(s) available.");
                }

                //Calculate price by reducing discount amount
                var discountAmount = product.Price * ( product.DiscountPercentage / 100);
                var itemPrice = product.Price - discountAmount;
                totalAmount += itemPrice * item.Quantity;

                //Reduce stock
                product.Stock -= item.Quantity;

                await _productRepository.UpdateAsync(product);

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Discount = product.DiscountPercentage
                });
            }

            var order = _mapper.Map<Order>(dto);
            order.UserId = userId;
            order.TotalAmount = totalAmount;
            order.PaymentStatus = PaymentStatus.Pending;
            order.OrderStatus = OrderStatus.OrderPlaced;
            order.Items = orderItems;

            await _orderRepository.AddAsync(order);
            await _cartItemRepository.ClearCartAsync(userId);

            return order.Id;

        }

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
            var order = await _orderRepository.GetByIdAsync(orderId);
            if(order == null)
            {
                throw new KeyNotFoundException("Order Not Fount");
            }
            
            if(order.UserId != userId)
            {
                throw new UnauthorizedException("UnAuthorized");
            }

            var orderItem = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if(orderItem == null)
            {
                throw new KeyNotFoundException("Order item not fount");
            }
            orderItem.OrderStatus = OrderStatus.Cancelled;
            orderItem.CancelledAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);

        }

        
        
    }
}
