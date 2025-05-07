using FluentValidation;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudios.Commands.CreateDentalStudio
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
					var existingDentalStudio = repository.GetByEncodedName(value).Result;
					if (existingDentalStudio != null)
					{
						context.AddFailure($"Name: '{value}' is not unique");
					}
				});

			RuleFor(c => c.Description)
				.NotEmpty().WithMessage("Description should contain at least one sign!");

			RuleFor(c => c.PhoneNumber)
				.MinimumLength(8)
				.MaximumLength(12);
		}
	}
}