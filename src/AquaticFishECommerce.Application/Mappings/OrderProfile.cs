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
                .ForMember(dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.ProductImage,
                    opt => opt.MapFrom(src =>
                        src.Product.Images
                            .Where(i => i.IsPrimary)
                            .Select(i => i.ImageUrl)
                            .FirstOrDefault()))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src =>
                        (src.Price - (src.Price * src.Discount / 100)) * src.Quantity));

            // Address Response
            CreateMap<Address, AddressResponseDto>();
                
        }
    }
}