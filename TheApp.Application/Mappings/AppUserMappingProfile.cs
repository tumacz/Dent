using AutoMapper;
using TheApp.Application.ApplicationUser.UserDTO;

namespace TheApp.Application.Mappings
{
    public class AppUserMappingProfile: Profile
    {
        public AppUserMappingProfile() 
        {
            CreateMap<Domain.Entities.ApplicationUser, AppUserDTO>();
        }
    }
}
