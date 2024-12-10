namespace TheApp.Domain.Entities
{
    public class UserWithRoles
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
