using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
    }
}