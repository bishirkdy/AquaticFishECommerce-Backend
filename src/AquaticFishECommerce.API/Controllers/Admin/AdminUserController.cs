using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/users")]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;

        }
     
        //Controller for get all users for admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
    }
}
