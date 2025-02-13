using FluentValidation;

namespace TheApp.Application.ApplicationUser.UserDTO.Commands
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        private readonly List<string> _availableRoles = new List<string> { "Administrator", "Moderator", "Owner" };

        public EditUserCommandValidator()
        {
            RuleFor(c => c.Roles)
                .Must(roles => roles == null || !roles.Any() || roles.All(role => _availableRoles.Contains(role)))
                .WithMessage("All roles must be valid and exist in the available roles.");
        }
    }
}
