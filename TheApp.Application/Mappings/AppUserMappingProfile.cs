using AutoMapper;
using TheApp.Application.ApplicationUser.UserDTO;
using TheApp.Domain.Entities;

namespace TheApp.Application.Mappings
{
    public class AppUserMappingProfile: Profile
    {
        public AppUserMappingProfile() 
        {
            CreateMap<AppUser, AppUserDTO>();
        }
    }
}
