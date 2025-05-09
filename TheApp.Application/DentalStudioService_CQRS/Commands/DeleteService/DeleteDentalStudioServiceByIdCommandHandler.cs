using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.DeleteService
{
	public class DeleteDentalStudioServiceByIdCommandHandler : IRequestHandler<DeleteDentalStudioServiceByIdCommand>
	{
		private readonly IUserContext _userContext;
		private readonly IDentalStudioServiceRepository _repository;

		public DeleteDentalStudioServiceByIdCommandHandler(IUserContext userContext, IDentalStudioServiceRepository dentalStudioServiceRepository)
		{
			_userContext = userContext;
			_repository = dentalStudioServiceRepository;
		}

		public async Task Handle(DeleteDentalStudioServiceByIdCommand request, CancellationToken cancellationToken)
		{
			var currentUser = _userContext.GetCurrentUser();
			if (currentUser == null)
			{
				throw new InvalidOperationException("Context user is not present.");
			}

			var service = await _repository.GetServiceById(request.Id);
			if (service == null)
			{
				throw new KeyNotFoundException($"Dental studio service with ID {request.Id} does not exist.");
			}

			var isAuthorized = currentUser.IsInRole("Moderator") ||
							   service.DentalStudio?.CreatedById == currentUser.Id;

			if (!isAuthorized)
			{
				throw new UnauthorizedAccessException("You are not authorized to delete this service.");
			}

			await _repository.DeleteDentalStudioService(request.Id);
		}
	}
}
