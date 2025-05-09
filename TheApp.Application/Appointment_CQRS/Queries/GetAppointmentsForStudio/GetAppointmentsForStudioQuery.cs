using MediatR;
using TheApp.Application.Appointment_CQRS.Queries;

namespace TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForStudio
{
	public class GetAppointmentsForStudioQuery : IRequest<IEnumerable<AppointmentDetailsDto>>
	{
		public string StudioEncodedName { get; set; } = default!;
	}
}