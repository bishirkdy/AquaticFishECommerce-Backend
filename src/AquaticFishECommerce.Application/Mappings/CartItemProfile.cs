using AquaticFishECommerce.Application.DTOs.CartItem;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, CartItemResponseDto>()
            .ForMember(d => d.CartItemId,
                o => o.MapFrom(s => s.Id))

            .ForMember(d => d.ProductId,
                o => o.MapFrom(s => s.ProductId))

            .ForMember(d => d.ProductName,
                o => o.MapFrom(s => s.Product.Name))

            .ForMember(d => d.OriginalPrice,
                o => o.MapFrom(s => s.Product.Price))

            .ForMember(d => d.DiscountPercentage,
                o => o.MapFrom(s => s.Product.DiscountPercentage))

            .ForMember(d => d.Quantity,
                o => o.MapFrom(s => s.Quantity))

            .ForMember(d => d.ImageUrl,
                o => o.MapFrom(s => s.Product.Images
                    .Where(i => i.IsPrimary)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()))

            .ForMember(d => d.AvailableStock,
            o => o.MapFrom(s => s.Product.Stock));
    }
}