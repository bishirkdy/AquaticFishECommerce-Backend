using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Response;
using AquaticFishECommerce.Application.DTOs.User;
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
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        // Register a new user
        public async Task RegisterAsync(RegisterUserDto dto)
        {
            // Check if the email already exists
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new ConflictException("Email already exists.");
            }

            var user = _mapper.Map<User>(dto);

            // Hash the password before saving
            user.PasswordHash = _passwordHasher.Hash(dto.Password);

            // Save the user
            await _userRepository.AddAsync(user);
        }

        // Login user and return access token
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Generate JWT access token
            var accessToken = _jwtService.GenerateAccessToken(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken
            };
        }

        // Get all users
        public async Task<IEnumerable<UserListDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserListDto>>(users);
        }

        // Get user by ID
        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new BadRequestException("Id is required");

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        // Update user information
        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            if (id == Guid.Empty)
                throw new BadRequestException("Id is required");

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            // Copy updated values
            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);
        }

        // Delete user
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new BadRequestException("Id is required");

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            await _userRepository.DeleteAsync(user);
        }
    }
}