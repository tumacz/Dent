namespace TheApp.Application.AppointmentDTO
{
    public class AppointmentDataTransferObject
    {
        public int Id { get; set; }
        public int DentalStudioServiceId { get; set; }
        public string ServiceName { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public int CreatedById { get; set; } = default!;
    }
}

