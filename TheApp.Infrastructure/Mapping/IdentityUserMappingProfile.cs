using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Mapping
{
    public class IdentityUserMappingProfile : Profile
    {
        public IdentityUserMappingProfile()
        {
            CreateMap<IdentityUser, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
