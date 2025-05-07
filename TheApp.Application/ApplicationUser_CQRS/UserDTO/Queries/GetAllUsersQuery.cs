using MediatR;

namespace TheApp.Application.ApplicationUser.UserDTO.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<AppUserDTO>>
    {
    }
}
