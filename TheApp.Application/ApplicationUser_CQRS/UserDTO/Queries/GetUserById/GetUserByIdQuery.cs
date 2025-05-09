using MediatR;

namespace TheApp.Application.ApplicationUser.UserDTO.Queries
{
	public class GetUserByIdQuery : IRequest<AppUserDTO>
	{
		public string Id { get; set; }

		public GetUserByIdQuery(string id)
		{
			Id = id;
		}
	}
}
