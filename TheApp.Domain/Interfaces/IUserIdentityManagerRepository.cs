using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IUserIdentityManagerRepository
    {
        Task<IEnumerable<UserWithRoles>> GetAllUsers();
    }
}
