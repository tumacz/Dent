using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
{
    public class DeleteDentalStudioServiceByIdCommandHandler : IRequestHandler<DeleteDentalStudioServiceByIdCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IDentalStudioServiceRepository _dentalStudioServiceRepository;

        public DeleteDentalStudioServiceByIdCommandHandler(IUserContext userContext, IDentalStudioServiceRepository dentalStudioServiceRepository)
        {
            _userContext = userContext;
            _dentalStudioServiceRepository = dentalStudioServiceRepository;
        }

        public async Task Handle(DeleteDentalStudioServiceByIdCommand request, CancellationToken cancellationToken)
        {
            var userContext = _userContext.GetCurrentUser();
            if (userContext != null && userContext.IsInRole("Moderator"))
            {
                await _dentalStudioServiceRepository.DeleteDentalStudioService(request.Id);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}