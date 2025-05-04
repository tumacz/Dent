using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user ?? throw new Exception($"User with ID {id} not found.");
        }

        public async Task<IEnumerable<string>> GetRolesForUser(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<string?>> GetAvailableRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => r.Name);
        }

        public async Task CommitEmail(ApplicationUser user, string newEmail)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to change email: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task CommitRoles(ApplicationUser user, IEnumerable<string> updatedUserRoles)
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
