namespace TheApp.Domain.Entities
{
    public class DentalStudioService
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
        public TimeSpan? Duration { get; set; } = default!;
        public int DentalStudioId { get; set; } =default!;
        public DentalStudio DentalStudio { get; set; } = default!;

        public int? DentalServiceId { get; set; } = default!;
        public DentalService? DentalService { get; set; } = default!;
        public List<Appointment> Appointments { get; set; } = new();

    }
}