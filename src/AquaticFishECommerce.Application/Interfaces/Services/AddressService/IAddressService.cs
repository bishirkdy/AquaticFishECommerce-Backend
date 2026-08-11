using AquaticFishECommerce.Application.DTOs.Address;


namespace AquaticFishECommerce.Application.Interfaces.Services.Address
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressResponseDto>> GetUserAddressesAsync(Guid userId);
        Task<AddressResponseDto> AddAddressAsync(Guid userId, CreateAddressDto dto);
        Task DeleteAddressAsync(Guid userId, Guid addressId);
        Task<AddressResponseDto?> GetLastUsedAddressAsync(Guid userId);

    }
}
