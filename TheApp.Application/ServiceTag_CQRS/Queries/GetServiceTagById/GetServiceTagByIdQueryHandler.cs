using AutoMapper;
using MediatR;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ServiceTag_CQRS.Queries
{
	public class GetServiceTagByIdQueryHandler : IRequestHandler<GetServiceTagByIdQuery, ServiceTagDTO.ServiceTagDTO>
	{
		private readonly IServiceTagRepository _repository;
		private readonly IMapper _mapper;

		public GetServiceTagByIdQueryHandler(IServiceTagRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<ServiceTagDTO.ServiceTagDTO> Handle(GetServiceTagByIdQuery request, CancellationToken cancellationToken)
		{
			var tag = await _repository.GetTagById(request.Id);
			return _mapper.Map<ServiceTagDTO.ServiceTagDTO>(tag);
		}
	}
}