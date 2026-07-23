using AquaticFishECommerce.Application.DTOs.Favorite;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Mappings
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteResponseDto>()
                .ForMember(d => d.FavoriteId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
                .ForMember(d => d.DiscountPercentage, o => o.MapFrom(s => s.Product.DiscountPercentage))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.Product.IsActive))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Product.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl));
        }
    }
}
