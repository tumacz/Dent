using Microsoft.AspNetCore.Identity;

namespace TheApp.Domain.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
        Task<IEnumerable<string>> GetRolesForUser(IdentityUser user);
        Task<IEnumerable<string?>> GetAvailableRoles();
    }
}
