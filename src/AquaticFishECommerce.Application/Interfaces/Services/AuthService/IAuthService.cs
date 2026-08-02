using AquaticFishECommerce.Application.DTOs.Auth;
using AquaticFishECommerce.Application.DTOs.User;


namespace AquaticFishECommerce.Application.Interfaces.Services.AuthService
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<UserDto?> GetByIdAsync(Guid id);
        Task ForgotPasswordAsync(ForgotPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task<RefreshTokenResponseDto> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(Guid userId);
    }
}
