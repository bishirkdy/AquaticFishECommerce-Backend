using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<IEnumerable<User>> GetAllAsyncUser();
        Task<User?> GetByRefreshTokenHashAsync(string refreshTokenHash);
        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string? search, string? status);
    }
}
