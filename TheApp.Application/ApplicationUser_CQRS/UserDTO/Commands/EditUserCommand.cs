using MediatR;
using System.Text.Json.Serialization;

namespace TheApp.Application.ApplicationUser.UserDTO.Commands
{
    public class EditUserCommand : IRequest
    {
		public string Id { get; set; } = default!;
		public string? Email { get; set; }
		public List<string> Roles { get; set; } = new();
	}
}
