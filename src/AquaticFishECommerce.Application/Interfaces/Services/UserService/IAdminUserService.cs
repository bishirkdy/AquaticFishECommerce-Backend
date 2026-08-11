using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.User;

namespace AquaticFishECommerce.Application.Interfaces.Services.User
{
    public interface IAdminUserService
    {
        Task<IEnumerable<UserListDto>> GetAllAsync();
        Task<bool> UpdateUserBlockStatusAsync(Guid userId, bool isBlocked);
        Task DeleteUserAsync(Guid userId);
        Task<PaginatedResponse<UserListDto>> GetUsersAsync(UserPaginatedQueryDto request);
    }
}
