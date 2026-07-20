using AquaticFishECommerce.Application.DTOs.Favorite;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public FavoriteService(
            IFavoriteRepository favoriteRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddFavoriteAsync(Guid userId, AddFavoriteDto dto)
        {
            var product = await _productRepository.GetByIdAsyn(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            if (!product.IsActive)
                throw new Exception("Product is unavailable.");

            var favorite = await _favoriteRepository
                .GetFavoriteAsync(userId, dto.ProductId);

            if (favorite != null)
                throw new Exception("Product already in favorites.");

            favorite = new Favorite
            {
                UserId = userId,
                ProductId = dto.ProductId
            };

            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task<FavoriteListResponseDto> GetFavoritesAsync(Guid userId)
        {
            var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId);
            var favoriteItems = _mapper.Map<List<FavoriteResponseDto>>(favorites);

            return new FavoriteListResponseDto
            {
                UserId = userId,
                Favorites = favoriteItems,
                TotalFavorites = favoriteItems.Count
            };
        }

        public async Task RemoveFavoriteAsync(Guid userId, Guid favoriteId)
        {
            var favorite = await _favoriteRepository.GetByIdAsyn(favoriteId);

            if (favorite == null)
                throw new Exception("Favorite not found.");

            if (favorite.UserId != userId)
                throw new Exception("Unauthorized.");

            await _favoriteRepository.DeleteAsync(favorite);
        }

        public async Task ClearFavoritesAsync(Guid userId)
        {
            await _favoriteRepository.ClearFavoritesAsync(userId);
        }
    }
}
