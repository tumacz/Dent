using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TheApp.Application.UsersDTO;
using TheApp.Application.UsersDTO.Commands.EditUser;
using TheApp.Domain.Entities;

public class UserDtoMappingProfile : Profile
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserDtoMappingProfile(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;

        CreateMap<UserWithRoles, UserDTO>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dto => dto.Roles, opt => opt.MapFrom(src => src.Roles));

        CreateMap<UserDTO, EditUserCommand>();
    }
}
