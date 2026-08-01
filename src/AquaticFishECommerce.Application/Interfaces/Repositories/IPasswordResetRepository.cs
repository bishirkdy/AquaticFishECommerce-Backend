using AquaticFishECommerce.Domain.Entities;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IPasswordResetRepository : IGenericRepository<PasswordResetToken>
    {
        Task<PasswordResetToken?> GetByTokenAsync(string token);
    }
}
