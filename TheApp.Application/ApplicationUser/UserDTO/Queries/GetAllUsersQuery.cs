using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.ApplicationUser.UserDTO.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<AppUserDTO>>
    {
    }
}
