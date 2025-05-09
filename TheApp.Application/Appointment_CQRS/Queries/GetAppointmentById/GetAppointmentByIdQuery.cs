using MediatR;
using TheApp.Application.Appointment_CQRS.Queries;

public class GetAppointmentByIdQuery : IRequest<AppointmentDetailsDto>
{
	public int Id { get; set; }

	public GetAppointmentByIdQuery(int id) => Id = id;
}