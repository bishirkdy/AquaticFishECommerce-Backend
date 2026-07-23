using AquaticFishECommerce.Application.DTOs.CartItem;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Mappings
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemResponseDto>()
              .ForMember(d => d.CartItemId, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
              .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
              .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
              .ForMember(d => d.TotalPrice,
                  o => o.MapFrom(s => s.Product.Price * s.Quantity))
              .ForMember(d => d.ImageUrl,
                  o => o.MapFrom(s => s.Product.Images
                      .FirstOrDefault(i => i.IsPrimary)!.ImageUrl));
        }
    }
}
