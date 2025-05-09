using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudios.Commands.EditDentalStudio
{
	public class EditDentalStudioCommandHandler : IRequestHandler<EditDentalStudioCommand>
	{
		private readonly IDentalStudioRepository _repository;
		private readonly IUserContext _userContext;

		public EditDentalStudioCommandHandler(IDentalStudioRepository repository, IUserContext userContext)
		{
			_repository = repository;
			_userContext = userContext;
		}

		public async Task Handle(EditDentalStudioCommand request, CancellationToken cancellationToken)
		{
			var user = _userContext.GetCurrentUser();
			var dentalStudio = await _repository.GetByEncodedName(request.EncodedName!);
			if (dentalStudio == null)
			{
				throw new KeyNotFoundException($"Dental studio with name: {request.Name} not found");
			}

			var isEditable = user != null && (dentalStudio.CreatedById == user.Id || user.IsInRole("Moderator"));
			if (!isEditable)
			{
				throw new UnauthorizedAccessException("You are not authorized to edit this dental studio");
			}

			dentalStudio.ContactDetails.PhoneNumber = request.PhoneNumber;
			dentalStudio.ContactDetails.Link = request.Link;
			dentalStudio.ContactDetails.City = request.City;
			dentalStudio.ContactDetails.PostalCode = request.PostalCode;
			dentalStudio.ContactDetails.Street = request.Street;
			dentalStudio.Description = request.Description;

			await _repository.Commit();
		}
	}
}