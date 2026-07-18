using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
namespace AquaticFishECommerce.Infrastructure.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new Exception("Email already exists.");
            }

            var user = _mapper.Map<User>(dto);

            await _userRepository.AddAsync(user);
        }



        public Task<string> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
