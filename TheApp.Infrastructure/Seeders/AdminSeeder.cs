using Microsoft.AspNetCore.Identity;

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
            var roleModerator = await _roleManager.RoleExistsAsync("Moderator");

            if (!roleModerator)
            {
                await _roleManager.CreateAsync(new IdentityRole("Moderator"));
            }

            var roleAdmin = await _roleManager.RoleExistsAsync("Administrator");

            if (!roleAdmin)
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            var adminUser = await _userManager.FindByNameAsync("Administrator@admin.com");

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "Administrator@admin.com", Email = "Administrator@admin.com" };
                var result = await _userManager.CreateAsync(adminUser, "Pa$$word1");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}