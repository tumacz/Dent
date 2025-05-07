using MediatR;
using AutoMapper;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceForEncodedNameQueryHandler : IRequestHandler<GetDentalStudioServiceForEncodedNameQuery, IEnumerable<DentalStudioServiceDataTransferObject>>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioServiceRepository _repository;

        public GetDentalStudioServiceForEncodedNameQueryHandler(IMapper mapper, IDentalStudioServiceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<DentalStudioServiceDataTransferObject>> Handle(GetDentalStudioServiceForEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var dentalStudioServices = await _repository.GetAllServicesByStudioEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<DentalStudioServiceDataTransferObject>>(dentalStudioServices);

            return dtos;
        }
    }
}