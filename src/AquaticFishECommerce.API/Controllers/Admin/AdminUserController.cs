using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;

        }
     
        //Controller for get all users for admin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _adminUserService.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<UserListDto>>
            {
                Success = true,
                Message = "All User featched successfully",
                Data = users
            });
        }


        //Controller for block and unblock user
        [HttpPatch("{id}/block")]
        public async Task<IActionResult> UpdateBlockStatus(Guid id,[FromBody] UserBlockDto dto)
        {
            var result = await _adminUserService.UpdateUserBlockStatusAsync(id, dto.IsBlocked);

            if (!result)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });

            return Ok(new ApiResponse
            {
                Success = true,
                Message = dto.IsBlocked
                    ? "User blocked successfully."
                    : "User unblocked successfully."
            });
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _adminUserService.DeleteUserAsync(userId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted successfully."
            });
        }
    }
}
