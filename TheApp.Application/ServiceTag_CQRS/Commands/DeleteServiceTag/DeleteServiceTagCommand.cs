using MediatR;

namespace TheApp.Application.ServiceTagDTO.Commands
{
	public class DeleteServiceTagCommand : IRequest
	{
		public int Id { get; set; }
	}
}