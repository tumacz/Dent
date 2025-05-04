using MediatR;

namespace TheApp.Application.AppointmentDTO.Commands.Appointment
{
    public class DeleteAppointmentCommand : IRequest
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
    }
}
