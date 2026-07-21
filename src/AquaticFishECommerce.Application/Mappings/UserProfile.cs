using System;
using System.Collections.Generic;
using System.Text;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserListDto>();
        }
    }
}
