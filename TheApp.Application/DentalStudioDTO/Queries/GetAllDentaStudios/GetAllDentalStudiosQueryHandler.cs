using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudios
{
    public class GetAllDentalStudiosQueryHandler : IRequestHandler<GetAllDentalStudiosQuery, IEnumerable<DentalStudioDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDentalStudioRepository _repository;

        public GetAllDentalStudiosQueryHandler(IMapper mapper, IDentalStudioRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<DentalStudioDTO>> Handle(GetAllDentalStudiosQuery request, CancellationToken cancellationToken)
        {
            var dentalStudios = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<DentalStudioDTO>>(dentalStudios);

            return dtos;
        }
    }
}
