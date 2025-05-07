namespace TheApp.Application.DentalStudio_CQRS.DataTransferObjects
{
    public class DentalStudioListItemDTO
    {
        public string Name { get; set; } = default!;
        public string EncodedName { get; set; } = default!;
        public string? City { get; set; }
    }
}