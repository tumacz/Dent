using FluentValidation;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.EditService
{
    public class EditDentalStudioServiceCommandValidator : AbstractValidator<EditDentalStudioServiceCommand>
    {
        public EditDentalStudioServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Service ID must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Cost)
                .NotEmpty()
                .WithMessage("Cost is required.")
                .Matches(@"^\d+(\.\d{1,2})?$")
                .WithMessage("Cost must be a valid number (e.g., 100 or 99.99).");

            RuleFor(x => x.Duration)
                .NotNull()
                .WithMessage("Duration is required.")
                .GreaterThan(TimeSpan.Zero)
                .WithMessage("Duration must be greater than 0.");
        }
    }
}