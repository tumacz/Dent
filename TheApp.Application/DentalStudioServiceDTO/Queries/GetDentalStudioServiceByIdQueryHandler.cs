using AutoMapper;
using MediatR;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceByIdQueryHandler : IRequestHandler<GetDentalStudioServiceByIdQuery, DentalStudioServiceDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioServiceRepository _repository;

        public GetDentalStudioServiceByIdQueryHandler(IMapper mapper, IDentalStudioServiceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DentalStudioServiceDTO> Handle(GetDentalStudioServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var dentalStudioServices = await _repository.GetById(request.Id);
            var dto = _mapper.Map<DentalStudioServiceDTO>(dentalStudioServices);

            return dto;
        }
    }
}