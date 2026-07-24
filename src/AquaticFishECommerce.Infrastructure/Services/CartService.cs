using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.CartItem;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartService(
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //Service for get cart item for user
        public async Task<CartResponseDto> GetCartAsync(Guid userId)
        {
            var cartItems = await _cartItemRepository.GetUserCartAsync(userId);

            var items = _mapper.Map<List<CartItemResponseDto>>(cartItems);

            return new CartResponseDto
            {
                UserId = userId,
                Items = items,
                TotalItems = items.Sum(x => x.Quantity),
                GrandTotal = items.Sum(x => x.TotalPrice)
            };
        }

        //Service for add to cart
        public async Task AddToCartAsyn(Guid userId, AddToCartDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (!product.IsActive)
                throw new KeyNotFoundException("Product is not available.");

            if (dto.Quantity > product.Stock)
                throw new BadRequestException("Insufficient stock.");

            var cartItem = await _cartItemRepository
                .GetCartItemAsync(userId, dto.ProductId);

            if (cartItem != null)
            {
                if (cartItem.Quantity + dto.Quantity > product.Stock)
                    throw new BadRequestException("Insufficient stock.");

                cartItem.Quantity += dto.Quantity;

                await _cartItemRepository.UpdateAsync(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                await _cartItemRepository.AddAsync(cartItem);
            }
        }

        //Service for update Quantity of cart item
        public async Task UpdateQuantityAsync(Guid userId, Guid cartItemId, UpdateCartItemDto dto)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);

            if (cartItem == null)
                throw new KeyNotFoundException("Cart item not found.");

            if (cartItem.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized.");

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (dto.Quantity > product.Stock)
                throw new BadRequestException("Insufficient stock.");

            cartItem.Quantity = dto.Quantity;

            await _cartItemRepository.UpdateAsync(cartItem);
        }

        //Service for remove item from cart
        public async Task RemoveItemAsync(Guid userId, Guid cartItemId)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);

            if (cartItem == null)
                throw new KeyNotFoundException("Cart item not found.");

            if (cartItem.UserId != userId)
                throw new Exception("Unauthorized.");

            await _cartItemRepository.DeleteAsync(cartItem);
        }

        //Service for clear user cart
        public async Task ClearCartAsync(Guid userId)
        {
            await _cartItemRepository.ClearCartAsync(userId);
        }
    }
}