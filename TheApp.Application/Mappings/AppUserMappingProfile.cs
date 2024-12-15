using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.ApplicationUser.UserDTO;

namespace TheApp.Application.Mappings
{
    public class AppUserMappingProfile: Profile
    {
        AppUserMappingProfile() 
        {
            //CreateMap<IdentityUser, AppUserDTO>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
