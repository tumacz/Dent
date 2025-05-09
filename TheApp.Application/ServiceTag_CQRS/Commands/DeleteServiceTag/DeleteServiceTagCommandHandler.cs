using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ServiceTagDTO.Commands
{
	public class DeleteServiceTagCommandHandler : IRequestHandler<DeleteServiceTagCommand>
	{
		private readonly IServiceTagRepository _repository;
		private readonly IUserContext _userContext;

		public DeleteServiceTagCommandHandler(IServiceTagRepository repository, IUserContext userContext)
		{
			_userContext = userContext;
			_repository = repository;
		}

		public async Task Handle(DeleteServiceTagCommand request, CancellationToken cancellationToken)
		{
			var currentUser = _userContext.GetCurrentUser();
			if (currentUser == null)
				throw new UnauthorizedAccessException("User is not authenticated.");

			if (!currentUser.IsInRole("Administrator"))
				throw new UnauthorizedAccessException("Only administrators can delete service tags.");

			await _repository.Delete(request.Id);
		}
	}
}