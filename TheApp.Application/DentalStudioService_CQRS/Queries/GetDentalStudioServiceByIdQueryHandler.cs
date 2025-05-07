using AutoMapper;
using MediatR;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceByIdQueryHandler : IRequestHandler<GetDentalStudioServiceByIdQuery, DentalStudioServiceDataTransferObject>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioServiceRepository _repository;

        public GetDentalStudioServiceByIdQueryHandler(IMapper mapper, IDentalStudioServiceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DentalStudioServiceDataTransferObject> Handle(GetDentalStudioServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var dentalStudioServices = await _repository.GetServiceById(request.Id);
            var dto = _mapper.Map<DentalStudioServiceDataTransferObject>(dentalStudioServices);

            return dto;
        }
    }
}