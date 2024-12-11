using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IUserIdentityManagerRepository
    {
        Task Commit(UserWithRoles user);
        Task<IEnumerable<UserWithRoles>> GetAllUsers();
        Task <UserWithRoles?> GetUserByName(string userName);
    }
}
