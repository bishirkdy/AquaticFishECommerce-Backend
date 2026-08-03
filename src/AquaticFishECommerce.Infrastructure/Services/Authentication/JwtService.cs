using AquaticFishECommerce.Application.Common.Settings;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        //IOptions<T> is a wrapper that provides strongly typed configuration values from appsettings.json.
        public JwtService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        //Method to generate token
        public string GenerateAccessToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()), //Represents the unique user ID.
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role , user.Role.ToString())
            };

            //Create secrect key after converted to bytes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //An object that combines the security key and the signing algorithm used to digitally sign the JWT
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Creates a new JWT token object.
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: credential
                );
            //JwtSecurityTokenHandler - .NET class used to create, serialize, read, and validate JSON Web Tokens
            return new JwtSecurityTokenHandler().WriteToken(token); //It converts the object into the compact JWT string.
        }

        //Service to generate random string for refresh token
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //Service for hash token of refresh token
        public string HashRefreshToken(string refreshToken)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(refreshToken);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
