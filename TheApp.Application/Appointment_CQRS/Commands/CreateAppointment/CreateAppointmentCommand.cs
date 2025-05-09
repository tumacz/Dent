using MediatR;
using TheApp.Application.Appointment_CQRS.Commands;

namespace TheApp.Application.AppointmentDTO.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : AppointmentDataTransferObject, IRequest<int>
    {
    }
}
