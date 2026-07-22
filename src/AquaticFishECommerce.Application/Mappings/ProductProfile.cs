using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product , ProductResponseDto>();
            CreateMap<CreateProductDto , Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
