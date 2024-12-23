using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Domain.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
        Task<IEnumerable<string>> GetRolesForUser(IdentityUser user);
        Task<IEnumerable<string?>> GetAvailableRoles();
        Task <IdentityUser> GetUserById(string id);
        Task CommitEmail(IdentityUser user, string newEmail);
        Task CommitRoles(IdentityUser user, IEnumerable<string> updatedUserRoles);
    }
}
