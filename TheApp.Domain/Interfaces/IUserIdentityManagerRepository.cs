using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Domain.Interfaces
{
    public interface IUserIdentityManagerRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
    }
}
