using MediatR;
using TheApp.Application.Appointment_CQRS.Queries;

namespace TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForCurrentUser
{
	public class GetAppointmentsForCurrentUserQuery : IRequest<IEnumerable<AppointmentDetailsDto>>
	{
	}
}