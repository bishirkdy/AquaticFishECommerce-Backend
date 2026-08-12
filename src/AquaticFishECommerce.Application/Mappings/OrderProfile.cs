using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Create Order
            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderItemDto, OrderItem>();

            // Order Response
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.ShippingAddress,
                    opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.OrderedAt,
                    opt => opt.MapFrom(src => src.CreatedAt));

            // Order Item Response
            CreateMap<OrderItem, OrderItemResponseDto>()

                .ForMember(dest => dest.ProductName,
                     opt => opt.MapFrom(src => src.Product.Name))

                .ForMember(dest => dest.OriginalPrice,
                     opt => opt.MapFrom(src => src.UnitPrice))

                .ForMember(dest => dest.DiscountedPrice,
                     opt => opt.MapFrom(src => src.DiscountedUnitPrice))

                .ForMember(dest => dest.ProductImage,
                     opt => opt.MapFrom(src =>
                         src.Product.Images
                           .Where(i => i.IsPrimary)
                           .Select(i => i.ImageUrl)
                           .FirstOrDefault()));

            // Address Response
            CreateMap<Address, AddressResponseDto>();
        }
    }
}