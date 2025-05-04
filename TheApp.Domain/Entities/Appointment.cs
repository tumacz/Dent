namespace TheApp.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DentalStudioServiceId { get; set; }
        public DentalStudioService DentalStudioService { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime => StartTime.Add(DentalStudioService.Duration ?? TimeSpan.Zero);
        //public string CreatedById { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public ApplicationUser Client { get; set; } = default!;

    }
}