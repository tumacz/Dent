using AutoMapper;
using MediatR;
using TheApp.Domain.Interfaces;
using TheApp.Application.DentalStudio.DataTransferObjects;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;

namespace TheApp.Application.DentalStudios.Queries.GetDentalStudioByEncodedName
{
    public class GetDentalStudioByEncodedNameQueryHandler : IRequestHandler<GetDentalStudioByEncodedNameQuery, DentalStudioDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly IDentalStudioRepository _repository;

		public GetDentalStudioByEncodedNameQueryHandler(IMapper mapper, IDentalStudioRepository repository)
		{
			_mapper = mapper;
			_repository = repository;
		}

		public async Task<DentalStudioDetailsDto> Handle(GetDentalStudioByEncodedNameQuery request, CancellationToken cancellationToken)
		{
			var dentalStudio = await _repository.GetByEncodedName(request.EncodedName);
			if (dentalStudio == null)
			{
				throw new KeyNotFoundException($"Dental studio with name {request.EncodedName} not found");
			}

			return _mapper.Map<DentalStudioDetailsDto>(dentalStudio);
		}
	}
}