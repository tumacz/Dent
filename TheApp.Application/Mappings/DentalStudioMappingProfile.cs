using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheApp.Application.ApplicationUser;
using TheApp.Application.DataTransferObjects;
using TheApp.Application.DataTransferObjects.Commands.EditDentalStudio;
using TheApp.Domain.Entities;

namespace TheApp.Application.Mappings
{
    public class DentalStudioMappingProfile : Profile
    {
        public DentalStudioMappingProfile(IUserContext userContext)
        {   
            var user = userContext.GetCurrentUser();

            CreateMap<DentalStudioDTO, DentalStudio>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(scr => new DentalStudioContactDetails()
                {
                    City = scr.City,
                    PostalCode = scr.PostalCode,
                    Street = scr.Street,
                    PhoneNumber = scr.PhoneNumber,
                    Link = scr.Link,
                }));

            CreateMap<DentalStudio, DentalStudioDTO>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(scr => user != null && scr.CreatedById == user.Id))
                .ForMember(dto => dto.City, opt => opt.MapFrom(scr => scr.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(scr => scr.ContactDetails.PostalCode))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(scr => scr.ContactDetails.Street))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(scr => scr.ContactDetails.PhoneNumber))
                .ForMember(dto => dto.Link, opt => opt.MapFrom(scr => scr.ContactDetails.Link));

            CreateMap<DentalStudioDTO, EditDentalStudioCommand>();
		}
    }
}
