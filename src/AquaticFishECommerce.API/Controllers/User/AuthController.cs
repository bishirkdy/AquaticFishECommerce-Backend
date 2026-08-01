using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Auth;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.AuthService;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //Controller for registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await _authService.RegisterAsync(dto);
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

            var response = await _authService.LoginAsync(dto);
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

        //Controller for get current user
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetById()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _authService.GetByIdAsync(Guid.Parse(id));

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "User fetched successfully",
                Data = user
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _authService.ForgotPasswordAsync(dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "If an account with that email exists, a password reset link has been sent."
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await _authService.ResetPasswordAsync(dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Password reset successfully."
            });
        }
    }
}
