using AquaticFishECommerce.Application.DTOs.Response;
using AquaticFishECommerce.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserListDto>> GetAllAsync();
        Task UpdateAsync(Guid id, UpdateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}
