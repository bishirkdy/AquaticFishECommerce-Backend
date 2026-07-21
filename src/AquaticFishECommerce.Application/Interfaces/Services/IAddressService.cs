using AquaticFishECommerce.Application.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<AddressResponseDto> AddAddressAsync(Guid userId, CreateAddressDto dto);
        //Task DeleteAddressAsync(Guid userId, Guid addressId);
    }
}
