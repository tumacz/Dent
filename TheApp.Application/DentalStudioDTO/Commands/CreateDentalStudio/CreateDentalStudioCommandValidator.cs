using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio
{
    public class CreateDentalStudioCommandValidator : AbstractValidator<CreateDentalStudioCommand>
    {
        public CreateDentalStudioCommandValidator(IDentalStudioRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Length(2, 20)
                .Custom((value, context) =>
                {
                    var existingDentalStudio = repository.GetByName(value).Result;
                    if (existingDentalStudio != null)
                    {
                        context.AddFailure($"nazwa: '{value}' nie jest unikalna");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Opis powinien zawierać co najmniej jeden znak!");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
