using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
	public interface IAppUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IEnumerable<string>> GetRolesForUser(ApplicationUser user);
        Task<IEnumerable<string?>> GetAvailableRoles();
        Task <ApplicationUser> GetUserById(string id);
        Task CommitEmail(ApplicationUser user, string newEmail);
        Task CommitRoles(ApplicationUser user, IEnumerable<string> updatedUserRoles);
    }
}