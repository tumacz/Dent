using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

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
        public ApplicationUser? CreatedBy { get; set; }

        public string EncodedName { get; private set; } = default!;

        public List<DentalStudioService> DentalStudioServices { get; set; } = new();

        public void EncodeName()
        {
            var temp = Regex.Replace(Name.ToLowerInvariant(), @"[^a-z0-9\s-]", "");
            EncodedName = Regex.Replace(temp, @"\s+", "-");
        }
    }
}