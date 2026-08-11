using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId);
        Task<Address?> GetLastUsedAddressAsync(Guid userId);
        Task<Address?> FindExistingAddressAsync(Guid userId, string email , string street, string post, string district, string state, string pincode, string? landmark);
    }
}
