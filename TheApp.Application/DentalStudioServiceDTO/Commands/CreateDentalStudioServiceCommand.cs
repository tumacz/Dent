using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
{
    public class CreateDentalStudioServiceCommand : DentalStudioServiceDTO, IRequest
    {
        public string DentalStudioEncodedName { get; set; } = default!;
    }
}
