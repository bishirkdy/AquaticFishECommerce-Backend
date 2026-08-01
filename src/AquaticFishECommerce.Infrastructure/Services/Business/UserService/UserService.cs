using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.User;


namespace AquaticFishECommerce.Infrastructure.Services.Business.UserService
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        // Update user 
        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            if (id == Guid.Empty)
                throw new BadRequestException("Id is required.");

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (dto.Name is not null)
                user.Name = dto.Name;

            if (dto.Phone is not null)
                user.Phone = dto.Phone;

            if (dto.Password is not null)
                user.PasswordHash = _passwordHasher.Hash(dto.Password);

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