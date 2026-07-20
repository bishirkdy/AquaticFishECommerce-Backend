using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Favorite
{
    public class FavoriteListResponseDto
    {
        public Guid UserId { get; set; }
        public List<FavoriteResponseDto> Favorites { get; set; } = [];
        public int TotalFavorites { get; set; }
    }
}
