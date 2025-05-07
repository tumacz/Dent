using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.DeleteService
{
    public class DeleteDentalStudioServiceByIdCommand : IRequest
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
    }
}
