using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.CreateService
{
    public class CreateDentalStudioServiceCommand : IRequest<int>
    {
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
        public TimeSpan Duration { get; set; }
        public int DentalServiceId { get; set; } // tag
        public string DentalStudioEncodedName { get; set; } = default!;
    }

}
