using MediatR;

namespace TheApp.Application.ServiceTag_CQRS.Commands.CreateServiceTag
{
    public class CreateServiceTagCommand : IRequest<int>
    {
        public string Name { get; set; } = default;
    }
}