using MediatR;
using AutoMapper;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceForEncodedNameQueryHandler : IRequestHandler<GetDentalStudioServiceForEncodedNameQuery, IEnumerable<DentalStudioServiceDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioServiceRepository _repository;

        public GetDentalStudioServiceForEncodedNameQueryHandler(IMapper mapper, IDentalStudioServiceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<DentalStudioServiceDTO>> Handle(GetDentalStudioServiceForEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var dentalStudioServices = await _repository.GetAllByEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<DentalStudioServiceDTO>>(dentalStudioServices);

            return dtos;
        }
    }
}
