using AquaticFishECommerce.Application.DTOs.User;

namespace AquaticFishECommerce.Application.Interfaces.Services.User
{
    public interface IUserService
    {
        Task UpdateAsync(Guid id, UpdateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}
