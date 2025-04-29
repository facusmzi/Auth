using Auth.Interfaces;
using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;

        public DatabaseSeeder(AppDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task SeedAsync()
        {
            // Apply any pending migrations
            await _context.Database.MigrateAsync();

            // Check if there are any users in the database
            if (!await _context.Users.AnyAsync())
            {
                // Create admin user
                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    DisplayName = "Admin User",
                    EmailVerified = true,
                    PasswordHash = _passwordService.HashPassword("Admin@123")
                };

                await _context.Users.AddAsync(adminUser);

                // Create test user
                var testUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user@example.com",
                    DisplayName = "Test User",
                    EmailVerified = true,
                    PasswordHash = _passwordService.HashPassword("User@123")
                };

                await _context.Users.AddAsync(testUser);

                await _context.SaveChangesAsync();
            }
        }
    }
}
