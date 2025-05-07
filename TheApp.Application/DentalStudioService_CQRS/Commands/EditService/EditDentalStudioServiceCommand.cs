using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.EditService
{
    public class EditDentalStudioServiceCommand :  IRequest
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
        public TimeSpan? Duration { get; set; } = default!;
    }
}