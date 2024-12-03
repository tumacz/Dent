using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
{
    public class CreateDentalStudioServiceCommandHandler : IRequestHandler<CreateDentalStudioServiceCommand>
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
        public async Task Handle(CreateDentalStudioServiceCommand request, CancellationToken cancellationToken)
        {
            var dentalStudio = await _dentalStudioRepository.GetByEncodedName(request.DentalStudioEncodedName!);
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (dentalStudio.CreatedById == user.Id || user.IsInRole("Moderator"));
            if (!isEdibable)
            {
                return;
            }

            var dentalStudioService = new TheApp.Domain.Entities.DentalStudioService()
            {
                Cost = request.Cost,
                Description = request.Description,
                DentalStudioId = dentalStudio.Id,
            };

            await _dentalStudioServiceRepository.Create(dentalStudioService);
        }
    }
}