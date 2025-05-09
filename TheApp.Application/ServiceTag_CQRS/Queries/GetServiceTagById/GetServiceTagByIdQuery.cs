using MediatR;

namespace TheApp.Application.ServiceTag_CQRS.Queries
{
	public class GetServiceTagByIdQuery : IRequest<ServiceTagDTO.ServiceTagDTO>
	{
		public int Id { get; }

		public GetServiceTagByIdQuery(int id)
		{
			Id = id;
		}
	}
}