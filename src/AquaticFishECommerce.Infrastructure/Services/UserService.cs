using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

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



        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid Email Or Password");

            return "Login Successfull";
        }

        public async Task<IEnumerable<UserListDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserListDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsyn(id);
            if (user == null)
                throw new Exception("User not found.");

            return _mapper.Map<UserDto>(user);

        }

        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsyn(id);

            if (user == null)
                throw new Exception("User not found.");

            _mapper.Map(dto, user);

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsyn(id);

            if (user == null)
                throw new Exception("User not found.");

            await _userRepository.DeleteAsync(user);
        }
    }
}
