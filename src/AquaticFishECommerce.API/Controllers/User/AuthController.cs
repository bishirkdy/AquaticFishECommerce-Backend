using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Auth;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.AuthService;
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

            // Access Token Cookie
            Response.Cookies.Append(
                "accessToken",
                response.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15)
                });

            // Refresh Token Cookie
            Response.Cookies.Append(
                "refreshToken",
                response.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Login Successful",
                Data = response.User
            });
        }

        //Refresh token controller
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _authService.RefreshTokenAsync(refreshToken!);

            // Access Token Cookie
            Response.Cookies.Append(
                "accessToken",
                response.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15)
                });

            Response.Cookies.Append(
                "refreshToken",
                response.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Token refreshed successfully",
                Data = response.AccessToken
            });
        }

        //Controller for logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            await _authService.LogoutAsync(Guid.Parse(userIdClaim.Value));

            Response.Cookies.Delete("accessToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Logout successful."
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

        //Controller for forgot password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _authService.ForgotPasswordAsync(dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "If an account with that email exists, a password reset link has been sent."
            });
        }

        //Controller for reset password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
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
