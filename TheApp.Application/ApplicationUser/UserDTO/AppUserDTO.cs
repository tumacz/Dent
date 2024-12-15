namespace TheApp.Application.ApplicationUser.UserDTO
{
    public class AppUserDTO
    {
        public AppUserDTO(string id, string userName, string email, List<string> roles)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles ?? new List<string>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
