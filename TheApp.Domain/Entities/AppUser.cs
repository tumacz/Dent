namespace TheApp.Domain.Entities
{
    public class AppUser
    {
        public string Id { get; set; } = default!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
