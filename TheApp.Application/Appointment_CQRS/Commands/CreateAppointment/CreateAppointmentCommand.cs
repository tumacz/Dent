using MediatR;

namespace TheApp.Application.AppointmentDTO.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : AppointmentDataTransferObject, IRequest<int>
    {
    }
}
