using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Domain.Enums;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Seed
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public DbInitializer(
            AppDbContext context,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task SeedAsync()
        {
            if(await _context.Users.AnyAsync(x => x.Role == UserRole.Admin))
            {
                return;
            }
            var admin = new User
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = _passwordHasher.Hash("ABcd@@11"),
                Role = UserRole.Admin
            };
            _context.Users.Add(admin);
            await _context.SaveChangesAsync();
        }
    }
}
