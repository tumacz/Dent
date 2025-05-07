using TheApp.Application.DentalStudioServiceDTO;

namespace TheApp.Application.DentalStudio_CQRS.DataTransferObjects
{
    public class DentalStudioDetailsDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }
        public string EncodedName { get; set; } = default!;
        public bool IsEditable { get; set; }
        public List<DentalStudioServiceDataTransferObject> Services { get; set; } = new();
    }
}