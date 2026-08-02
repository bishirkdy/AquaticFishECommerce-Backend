

namespace AquaticFishECommerce.Application.DTOs.Auth
{
    public class RefreshTokenResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
