using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;
using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.CreateService
{
    public class CreateDentalStudioServiceCommandHandler : IRequestHandler<CreateDentalStudioServiceCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IDentalStudioRepository _dentalStudioRepository;
        private readonly IDentalStudioServiceRepository _dentalStudioServiceRepository;

        public CreateDentalStudioServiceCommandHandler(IUserContext userContext, IDentalStudioRepository dentalStudioRepository, IDentalStudioServiceRepository dentalStudioServiceRepository)
        {
            _userContext = userContext;
            _dentalStudioRepository = dentalStudioRepository;
            _dentalStudioServiceRepository = dentalStudioServiceRepository;
        }

        public async Task<int> Handle(CreateDentalStudioServiceCommand request, CancellationToken cancellationToken)
        {
            var dentalStudio = await _dentalStudioRepository.GetByEncodedName(request.DentalStudioEncodedName!);
            if (dentalStudio == null)
            {
                throw new KeyNotFoundException($"Dental studio with name {request.DentalStudioEncodedName} not found");
            }

            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (dentalStudio.CreatedById == user.Id || user.IsInRole("Moderator"));
            if (!isEdibable)
            {
                throw new UnauthorizedAccessException();
            }

            var dentalStudioService = new Domain.Entities.DentalStudioService()
            {
                Cost = request.Cost,
                Description = request.Description,
                DentalStudioId = dentalStudio.Id,
            };

            await _dentalStudioServiceRepository.Create(dentalStudioService);

            return dentalStudioService.Id;
        }
    }
}