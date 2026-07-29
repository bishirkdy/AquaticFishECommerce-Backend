using AquaticFishECommerce.Application.DTOs.User;

namespace AquaticFishECommerce.Application.Interfaces.Services.User
{
    public interface IAdminUserService
    {
        Task<IEnumerable<UserListDto>> GetAllAsync();

    }
}
