using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateProductDto , Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
