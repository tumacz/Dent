using MediatR;

namespace TheApp.Application.ServiceTag_CQRS.Queries.GetAllServiceTags
{
	public class GetAllServiceTagsQuery : IRequest<IEnumerable<ServiceTagDTO.ServiceTagDTO>>//??? spaces
	{

	}
}
