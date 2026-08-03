using AquaticFishECommerce.Application.DTOs.User;

public class AuthResponseDto
{
    public UserDto User { get; set; } = default;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}