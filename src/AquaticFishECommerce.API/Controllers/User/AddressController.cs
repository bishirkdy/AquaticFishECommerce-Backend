using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Application.Interfaces.Services.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }


        // Controller for create address
        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateAddressDto dto)
        {
            var address = await _addressService.AddAddressAsync(UserId, dto);

            return Ok(new ApiResponse<AddressResponseDto>
            {
                Success = true,
                Message = "Address added successfully.",
                Data = address
            });
        }

        //Controller for delete address
        [HttpDelete("{addressId:guid}")]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            await _addressService.DeleteAddressAsync(UserId, addressId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Address deleted successfully."
            });
        }

        //Controller for get user addresses
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserAddresses()
        {
            var addresses = await _addressService.GetUserAddressesAsync(UserId);

            return Ok(new ApiResponse<IEnumerable<AddressResponseDto>>
            {
                Success = true,
                Message = "Addresses retrieved successfully.",
                Data = addresses
            });
        }

        //Controller for get last address of user used
        [HttpGet("last-used")]
        public async Task<IActionResult> GetLastUsedAddress()
        {
            var address = await _addressService.GetLastUsedAddressAsync(UserId);
            if (address == null)
                return NotFound(new { message = "No address found." });

            return Ok(new ApiResponse<AddressResponseDto>
            {
                Success = true,
                Message = "Last used address fetched successfully",
                Data = address
            });
        }
    }
}