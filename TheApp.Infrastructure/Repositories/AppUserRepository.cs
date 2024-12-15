using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Interfaces;

namespace TheApp.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            return allUsers;
        }

        public async Task<IEnumerable<string>> GetRolesForUser(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IEnumerable<string?>> GetAvailableRoles()
        {
            var availableRolesIdentity = await _roleManager.Roles.ToListAsync();
            IEnumerable<string?> availableRoles = availableRolesIdentity.Select(role => role.Name);
            return availableRoles;
        }
    }
}
