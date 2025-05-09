using MediatR;
using AutoMapper;
using TheApp.Domain.Interfaces;
using TheApp.Application.DentalStudioServiceDTO;
using TheApp.Application.ApplicationUser;

namespace TheApp.Application.DentalStudioService_CQRS.Queries.GetServiceForEncodedName
{
    public class GetDentalStudioServiceForEncodedNameQueryHandler : IRequestHandler<GetDentalStudioServiceForEncodedNameQuery, IEnumerable<DentalStudioServiceDataTransferObject>>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioServiceRepository _repository;
		private readonly IUserContext _userContext;

        public GetDentalStudioServiceForEncodedNameQueryHandler(IMapper mapper, IDentalStudioServiceRepository repository, IUserContext userContext)
        {
            _mapper = mapper;
            _repository = repository;
			_userContext = userContext;
        }

		public async Task<IEnumerable<DentalStudioServiceDataTransferObject>> Handle(GetDentalStudioServiceForEncodedNameQuery request, CancellationToken cancellationToken)
		{
			var user = _userContext.GetCurrentUser();
			var services = await _repository.GetAllServicesByStudioEncodedName(request.EncodedName);

			var dtos = _mapper.Map<IEnumerable<DentalStudioServiceDataTransferObject>>(services).ToList();

			foreach (var dto in dtos)
			{
				var service = services.First(s => s.Id == dto.Id);
				dto.IsEditable = user != null && (
					service.DentalStudio?.CreatedById == user.Id || user.IsInRole("Moderator")
				);
			}

			return dtos;
		}
	}
}