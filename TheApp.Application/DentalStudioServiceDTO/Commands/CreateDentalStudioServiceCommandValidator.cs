using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
{
    public class CreateDentalStudioServiceCommandValidator: AbstractValidator<CreateDentalStudioServiceCommand>
    {
        public CreateDentalStudioServiceCommandValidator()
        {
            RuleFor(s => s.Cost).NotEmpty().NotNull();
            RuleFor(s => s.Description).NotEmpty().NotNull();
            RuleFor(s => s.DentalStudioEncodedName).NotEmpty().NotNull();
        }
    }
}
