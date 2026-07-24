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
    }
}