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
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository , IMapper mapper , IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new Exception("Email already exists.");
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _userRepository.AddAsync(user);
        }



        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);ty
            if (user == null)
                throw new Exception("Invalid Email Or Password");
            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValid)
                throw new Exception("Invalid Email or Password");
            var accessToken = _jwtService.GenerateAccessToken(user);
            return accessToken;
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
