using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.External
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        string HashRefreshToken(string refreshToken);
    }
}
