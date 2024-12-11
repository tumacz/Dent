using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Infrastructure.Repositories
{
    public class UserIdentityManagerRepository : IUserIdentityManagerRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserIdentityManagerRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Commit(UserWithRoles user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            if (identityUser == null)
            {
                throw new InvalidOperationException($"User with ID {user.Id} not found.");
            }

            identityUser.Email = user.Email;
            identityUser.UserName = user.UserName;

            var updateResult = await _userManager.UpdateAsync(identityUser);
            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user details: " +
                    string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            var currentRoles = await _userManager.GetRolesAsync(identityUser);

            var rolesToRemove = currentRoles.Except(user.Roles).ToList();
            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(identityUser, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to remove roles: " +
                        string.Join(", ", removeResult.Errors.Select(e => e.Description)));
                }
            }

            var rolesToAdd = user.Roles.Except(currentRoles).ToList();
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(identityUser, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to add roles: " +
                        string.Join(", ", addResult.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task<IEnumerable<UserWithRoles>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersWithRoles = new List<UserWithRoles>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                usersWithRoles.Add(new UserWithRoles
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return usersWithRoles;
        }

        public async Task<UserWithRoles?> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null) 
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userWithRoles = new UserWithRoles
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                Roles = roles.ToList()
            };

            return userWithRoles;
        }
    }
}
