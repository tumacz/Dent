using MediatR;
using System.Text.Json.Serialization;

namespace TheApp.Application.ApplicationUser.UserDTO.Commands
{
    public class EditUserCommand : AppUserDTO, IRequest
    {
        [JsonIgnore]
        public override List<string> AvailableRoles { get; set; } = new();
    }
}
