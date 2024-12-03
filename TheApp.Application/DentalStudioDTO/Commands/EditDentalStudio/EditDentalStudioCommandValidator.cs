using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Commands.EditDentalStudio
{
    public class EditDentalStudioCommandValidator : AbstractValidator<EditDentalStudioCommand>
    {
        public EditDentalStudioCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Opis powinien zawierać co najmniej jeden znak!");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
