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

        public async Task<IEnumerable<UserWithRoles>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersWithRoles = new List<UserWithRoles>();

            foreach (var user in users)
            {
                // Pobranie ról dla danego użytkownika
                var roles = await _userManager.GetRolesAsync(user);

                // Stworzenie UserWithRoles i przypisanie danych
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
    }
}
