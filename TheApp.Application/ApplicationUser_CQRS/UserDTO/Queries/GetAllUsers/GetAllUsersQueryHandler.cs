using MediatR;
using TheApp.Application.ApplicationUser.UserDTO;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ApplicationUser_CQRS.UserDTO.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<AppUserDTO>>
    {
        private readonly IAppUserRepository _userRepository;

        public GetAllUsersQueryHandler(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            var usersWithRoles = new List<AppUserDTO>();

            var availableRoles = await _userRepository.GetAvailableRoles();

            foreach (var user in users)
            {
                var roles = await _userRepository.GetRolesForUser(user);
                usersWithRoles.Add(new AppUserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                    AvailableRoles = availableRoles.ToList()!
                });
            }

            return usersWithRoles;
        }
    }
}