using AquaticFishECommerce.Application.Interfaces.Services;
namespace AquaticFishECommerce.Infrastructure.Services.Authentication
{
    //It is included method to hash and verity password by BCrypt
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password , string verifyPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, verifyPassword);
        }
    }
}
