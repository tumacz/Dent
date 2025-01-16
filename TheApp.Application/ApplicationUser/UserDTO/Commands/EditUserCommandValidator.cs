using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using TheApp.Domain.Interfaces;

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
