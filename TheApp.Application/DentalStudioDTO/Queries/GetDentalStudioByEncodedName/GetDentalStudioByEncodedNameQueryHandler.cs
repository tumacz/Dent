using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Queries.GetDentalStudioByEncodedName
{
	public class GetDentalStudioByEncodedNameQueryHandler : IRequestHandler<GetDentalStudioByEncodedNameQuery, DentalStudioDTO>
	{
		private readonly IMapper _mapper;
		private readonly IDentalStudioRepository _repository;

		public GetDentalStudioByEncodedNameQueryHandler(IMapper mapper, IDentalStudioRepository repository)
		{
			_mapper = mapper;
			_repository = repository;
		}

		public async Task<DentalStudioDTO> Handle(GetDentalStudioByEncodedNameQuery request, CancellationToken cancellationToken)
		{
			var dentalStudio = await _repository.GetByEncodedName(request.EncodedName);
			var dto = _mapper.Map<DentalStudioDTO>(dentalStudio);

			return dto;
		}
	}
}
