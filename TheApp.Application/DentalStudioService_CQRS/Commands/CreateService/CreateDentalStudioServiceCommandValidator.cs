using FluentValidation;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.CreateService
{
    public class CreateDentalStudioServiceCommandValidator : AbstractValidator<CreateDentalStudioServiceCommand>
    {
        public CreateDentalStudioServiceCommandValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage("Service description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(s => s.Cost)
                .NotEmpty().WithMessage("Service cost is required.")
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Cost must be a valid number (e.g., 100 or 99.99).");

            RuleFor(s => s.DentalStudioEncodedName)
                .NotEmpty().WithMessage("Studio identifier (encoded name) is required.")
                .MaximumLength(100).WithMessage("Encoded name is too long.");

            RuleFor(s => s.Duration)
                .NotNull().WithMessage("Service duration is required.")
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");

            RuleFor(s => s.DentalServiceId)
                .NotNull().WithMessage("Service tag is required.");
        }
    }
}
