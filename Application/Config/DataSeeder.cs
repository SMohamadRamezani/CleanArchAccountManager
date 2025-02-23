using Microsoft.AspNetCore.Identity;
using App.Interfaces;
using Domain.Models.User;

namespace ApiEndPoint.Config
{
    public static class DataSeeder
    {
        public static async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var jwtGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();

            string defaultEmail = "admin@example.com";
            string defaultPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(defaultEmail);
            if (user == null)
            {
                var role = new Role { Id = "9f6ce344-680c-4976-8ab9-eb57bd357ae5", Name = "admin", NormalizedName = "ADMIN" };
                user = new User
                {
                    UserName = "admin",
                    Email = defaultEmail,
                    EmailConfirmed = true,
                    RefreshToken = jwtGenerator.GenerateRefreshToken(),
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                    Role = role
                };

                await userManager.CreateAsync(user, defaultPassword);

                // ایجاد توکن JWT برای کاربر
                var token = jwtGenerator.GenerateToken(user);
                Console.WriteLine($"Generated Default JWT Token: {token}");

                // ذخیره توکن جدید در دیتابیس
                user.RefreshToken = jwtGenerator.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await userManager.UpdateAsync(user);
            }
        }
    }
}
