using AquaticFishECommerce.Application.Common.Settings;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Authentication
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

            //Create secrect key after converted to bytes from jwtsettings
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
            //JwtSecurityTokenHandler is a .NET class used to create, serialize, read, and validate JSON Web Tokens
            return new JwtSecurityTokenHandler().WriteToken(token); //It converts the object into the compact JWT string.
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
