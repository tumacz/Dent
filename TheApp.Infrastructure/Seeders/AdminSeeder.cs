using Microsoft.AspNetCore.Identity;
using TheApp.Infrastructure.Migrations;

namespace TheApp.Infrastructure.Seeders
{
    public class AdminSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            var roleExists = await _roleManager.RoleExistsAsync("Moderator");

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Moderator"));
            }

            var adminUser = await _userManager.FindByNameAsync("moderator@admin.com");

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "moderator@admin.com", Email = "moderator@admin.com" };
                var result = await _userManager.CreateAsync(adminUser, "Pa$$word1");
        
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Moderator");
                }
            }
        }
    }
}