using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
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
            var userContext = _userContext.GetCurrentUser();
            if (userContext == null)
            {
                throw new InvalidOperationException("Context user is not present");
            }

            if (!(userContext.IsInRole("Moderator") || userContext.Id == request.Id.ToString()))
            {
                throw new InvalidOperationException("You are not authorized to delete this service.");
            }

            var service = await _repository.GetById(request.Id);
            if (service == null)
            {
                throw new KeyNotFoundException($"Dental studio service with ID {request.Id} does not exist.");
            }

            await _repository.DeleteDentalStudioService(request.Id);
        }
    }
}
