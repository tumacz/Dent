using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TheApp.Application.UsersDTO;

public class UserDtoMappingProfile : Profile
{
    private readonly UserManager<IdentityUser> _userManager;

    // Konstruktor przyjmujący UserManager
    public UserDtoMappingProfile(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;

        CreateMap<IdentityUser, UserDTO>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email));
    }
}
