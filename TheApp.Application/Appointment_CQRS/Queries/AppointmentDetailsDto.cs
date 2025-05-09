namespace TheApp.Application.Appointment_CQRS.Queries
{
	public class AppointmentDetailsDto
	{
		public int Id { get; set; }
		public string ServiceName { get; set; } = default!;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string StudioEncodedName { get; set; } = default!;
		public bool IsEditable { get; set; }
	}
}