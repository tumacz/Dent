using FluentValidation;
using TheApp.Application.ApplicationUser.UserDTO.Commands;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotNull().WithMessage("Email jest wymagany.")
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Niepoprawny format adresu e-mail.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Email musi zawierać poprawną domenę.");
    }
}