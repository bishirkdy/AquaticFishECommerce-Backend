using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly IValidator<LoginDto> _loginValidator;
        //private readonly IValidator<UpdateUserDto> _updateUserValidator;
        public UserController(IUserService userService)
        {
            _userService = userService;
            //_loginValidator = loginValidator;
            //_updateUserValidator = updateUserValidator;
        }
        //Controller for registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            //var validator = await _registerValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //{
            //    return BadRequest(validator.Errors);
            //}
            await _userService.RegisterAsync(dto);
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "User registered successfully",
                Success = true
            });
        }
        //Controller for login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            //var validation = await _loginValidator.ValidateAsync(dto);
            //if (!validation.IsValid)
            //{
            //    return BadRequest(validation.Errors);
            //}
            var response = await _userService.LoginAsync(dto);
            Response.Cookies.Append(
                "accessToken",
                response.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(60)
                });
            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Login Successfull",
                Data = response.User
            });
        }
        //Controller for get all users for admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<UserListDto>>
            {
                Success = true,
                Message = "All User featched successfully",
                Data = users
            });
        }

        //Controller for get current user
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetById()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetByIdAsync(Guid.Parse(id));

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "User fetched successfully",
                Data = user
            });
        }

        //Controller for Update User taken by id and update
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            //var validator = await _updateUserValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //{
            //    return BadRequest(validator.Errors);
            //}

            await _userService.UpdateAsync(id, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User updated successfully.",
            });
        }

        //Controller for Delete User taken by id and delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted successfully.",
            });
        }
    }
}
