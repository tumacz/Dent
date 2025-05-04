using MediatR;

namespace TheApp.Application.AppointmentDTO.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : AppointmentDTO, IRequest<int>
    {
    }
}
