namespace TheApp.Application.Appointment_CQRS.Commands
{
    public class AppointmentDataTransferObject
    {
        public int Id { get; set; }
        public int DentalStudioServiceId { get; set; }
        public string ServiceName { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
    }
}