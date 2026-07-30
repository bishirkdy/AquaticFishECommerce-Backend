using AquaticFishECommerce.Application.DTOs.Review;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Application.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReviewResponseDto>()
            .ForMember(
             dest => dest.UserName,
            opt => opt.MapFrom(src => src.User.Name)
            );

        }
    }
}
