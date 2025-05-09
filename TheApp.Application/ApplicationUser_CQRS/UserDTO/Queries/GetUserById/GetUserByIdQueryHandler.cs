using MediatR;
using TheApp.Application.ApplicationUser.UserDTO;
using TheApp.Application.ApplicationUser.UserDTO.Queries;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ApplicationUser_CQRS.UserDTO.Queries
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AppUserDTO>
	{
		private readonly IAppUserRepository _userRepository;

		public GetUserByIdQueryHandler(IAppUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<AppUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserById(request.Id);
			if (user == null)
				throw new KeyNotFoundException($"User with ID '{request.Id}' not found.");

			var roles = await _userRepository.GetRolesForUser(user);
			var availableRoles = await _userRepository.GetAvailableRoles();

			return new AppUserDTO
			{
				Id = user.Id,
				Email = user.Email,
				UserName = user.UserName,
				Roles = roles.ToList(),
				AvailableRoles = availableRoles.ToList()
			};
		}
	}
}
