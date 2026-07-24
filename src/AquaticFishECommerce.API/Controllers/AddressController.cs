using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("Invalid user.");

            return Guid.Parse(userId);
        }

        // Controller for create address
        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateAddressDto dto)
        {
            var userId = GetUserId();

            var address = await _addressService.AddAddressAsync(userId, dto);

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
            var userId = GetUserId();

            await _addressService.DeleteAddressAsync(userId, addressId);

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
            var userId = GetUserId();

            var addresses = await _addressService.GetUserAddressesAsync(userId);

            return Ok(new ApiResponse<IEnumerable<AddressResponseDto>>
            {
                Success = true,
                Message = "Addresses retrieved successfully.",
                Data = addresses
            });
        }
    }
}