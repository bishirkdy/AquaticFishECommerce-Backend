using AquaticFishECommerce.Application.DTOs.Favorite;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Application.Mappings
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteResponseDto>()
                .ForMember(d => d.FavoriteId,
                    o => o.MapFrom(s => s.Id))

                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.ProductId))

                .ForMember(d => d.Name,
                    o => o.MapFrom(s => s.Product.Name))

                .ForMember(d => d.OriginalPrice,
                    o => o.MapFrom(s => s.Product.Price))

                .ForMember(d => d.DiscountPercentage,
                    o => o.MapFrom(s => s.Product.DiscountPercentage))

                .ForMember(d => d.IsActive,
                    o => o.MapFrom(s => s.Product.IsActive))
                .ForMember(d => d.Stock , 
                    o => o.MapFrom(s => s.Product.Stock))
                .ForMember(d => d.ImageUrl,
                    o => o.MapFrom(s =>
                        s.Product.Images
                         .Where(i => i.IsPrimary)
                         .Select(i => i.ImageUrl)
                         .FirstOrDefault()));
        }
    }
}
