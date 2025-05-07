namespace TheApp.Application.DentalStudioServiceDTO
{
    public class DentalStudioServiceDataTransferObject
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
        public TimeSpan? Duration { get; set; }
        public string ServiceTagName { get; set; } = default!;
        public string StudioEncodedName { get; set; } = default!;
    }
}
