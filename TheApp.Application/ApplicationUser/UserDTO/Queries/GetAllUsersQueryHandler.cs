using AutoMapper;
using MediatR;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ApplicationUser.UserDTO.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<AppUserDTO>>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IAppUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            var usersWithRoles = new List<AppUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userRepository.GetRolesForUser(user);
                usersWithRoles.Add(new AppUserDTO(user.Id, user.UserName, user.Email, roles.ToList()));
            }

            return usersWithRoles;
        }
    }
}