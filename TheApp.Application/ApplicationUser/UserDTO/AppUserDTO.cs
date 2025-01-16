﻿namespace TheApp.Application.ApplicationUser.UserDTO
{
    public class AppUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<string> AvailableRoles { get; set; }
    }
}
