using AquaticFishECommerce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services
{
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
