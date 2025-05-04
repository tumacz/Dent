using Microsoft.AspNetCore.Identity;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Seeders
{
    public class AdminSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            const string adminEmail = "Administrator@admin.com";
            const string adminPassword = "Pa$$word1";

            if (!await _roleManager.RoleExistsAsync("Moderator"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Moderator"));
            }

            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAdmin, "Administrator");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Błąd tworzenia użytkownika administratora: {errors}");
                }
            }
        }
    }
}
