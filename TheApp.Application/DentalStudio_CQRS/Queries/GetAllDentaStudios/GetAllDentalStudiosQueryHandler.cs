using AutoMapper;
using MediatR;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudios.Queries.GetAllDentalStudios
{
    public class GetAllDentalStudiosQueryHandler : IRequestHandler<GetAllDentalStudiosQuery, IEnumerable<DentalStudioListItemDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IDentalStudioRepository _repository;

		public GetAllDentalStudiosQueryHandler(IMapper mapper, IDentalStudioRepository repository)
		{
			_mapper = mapper;
			_repository = repository;
		}

		public async Task<IEnumerable<DentalStudioListItemDTO>> Handle(GetAllDentalStudiosQuery request, CancellationToken cancellationToken)
		{
			var studios = await _repository.GetAll();
			return _mapper.Map<IEnumerable<DentalStudioListItemDTO>>(studios);
		}
	}
}
