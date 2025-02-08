using Microsoft.AspNetCore.Identity;

namespace TheApp.Domain.Entities
{
    public class DentalStudio
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DentalStudioContactDetails ContactDetails { get; set; } = default!;

        public string? CreatedById {  get; set; } 
        public IdentityUser? CreatedBy { get; set; }

        public string EncodedName { get; private set; } = default!;

        public List<DentalStudioService> DentalStudioServices { get; set; } = new();

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}