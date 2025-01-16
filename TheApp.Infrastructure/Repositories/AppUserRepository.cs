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

        public async Task<IdentityUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user!;
        }

        public async Task CommitEmail(IdentityUser user, string newEmail)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to change email: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task CommitRoles(IdentityUser user, IEnumerable<string> updatedUserRoles)
        {
            var currentRoles = await _userManager.GetRolesAsync(user) ?? new List<string>();

            updatedUserRoles = updatedUserRoles ?? new List<string>();

            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    throw new Exception($"Failed to remove roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                }
            }

            if (updatedUserRoles.Any())
            {
                foreach (var role in updatedUserRoles)
                {
                    var addResult = await _userManager.AddToRoleAsync(user, role);
                    if (!addResult.Succeeded)
                    {
                        throw new Exception($"Failed to add role {role}: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

    }
}
