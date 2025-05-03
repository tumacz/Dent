namespace TheApp.Application.ApplicationUser.UserDTO
{
    public class AppUserDTO
    {
        public string Id { get; set; } = default!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public virtual List<string> AvailableRoles { get; set; } = new List<string>();
    }
}
