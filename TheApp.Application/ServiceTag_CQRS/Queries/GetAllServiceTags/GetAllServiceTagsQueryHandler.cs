using AutoMapper;
using MediatR;
using TheApp.Application.ServiceTag_CQRS.Queries.GetAllServiceTags;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ServiceTagDTO.Queries
{
	public class GetAllServiceTagsQueryHandler : IRequestHandler<GetAllServiceTagsQuery, IEnumerable<ServiceTagDTO>>
	{
		private readonly IServiceTagRepository _repository;
		private readonly IMapper _mapper;

		public GetAllServiceTagsQueryHandler(IServiceTagRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ServiceTagDTO>> Handle(GetAllServiceTagsQuery request, CancellationToken cancellationToken)
		{
			var tags = await _repository.GetAllTags();
			return _mapper.Map<IEnumerable<ServiceTagDTO>>(tags);
		}
	}
}