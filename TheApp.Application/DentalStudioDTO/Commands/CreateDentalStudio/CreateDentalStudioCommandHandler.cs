using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio
{
    public class CreateDentalStudioCommandHandler : IRequestHandler<CreateDentalStudioCommand>
    {
        private readonly IDentalStudioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public CreateDentalStudioCommandHandler(IMapper mapper, IDentalStudioRepository repository, IUserContext userContext) 
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }
        public async Task Handle(CreateDentalStudioCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Administrator"))
            {
                return;
            }
            var dentalStudio = _mapper.Map<DentalStudio>(request);
            dentalStudio.EncodeName();

            dentalStudio.CreatedById = currentUser.Id;
            await _repository.Create(dentalStudio);
        }
    }
}
