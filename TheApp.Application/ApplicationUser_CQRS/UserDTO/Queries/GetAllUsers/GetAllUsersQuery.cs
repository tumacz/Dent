using MediatR;
using TheApp.Application.ApplicationUser.UserDTO;

namespace TheApp.Application.ApplicationUser_CQRS.UserDTO.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<AppUserDTO>>
    {
    }
}
