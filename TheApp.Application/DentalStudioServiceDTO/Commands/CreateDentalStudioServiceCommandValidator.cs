using FluentValidation;

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
