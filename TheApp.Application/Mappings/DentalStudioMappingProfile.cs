using AutoMapper;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Entities;
using TheApp.Application.DentalStudio.DataTransferObjects;
using TheApp.Application.DentalStudios.Commands.EditDentalStudio;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;

namespace TheApp.Application.Mappings
{
    public class DentalStudioMappingProfile : Profile
	{
		public DentalStudioMappingProfile(IUserContext userContext)
		{
			var user = userContext.GetCurrentUser();

			// Entity -> Full DTO (used for commands)
			CreateMap<Domain.Entities.DentalStudio, DentalStudioDataTransferObject>()
				.ForMember(dto => dto.IsEditable, opt => opt.MapFrom(scr =>
					user != null && (scr.CreatedById == user.Id || user.Roles.Contains("Moderator")))
				)
				.ForMember(dto => dto.City, opt => opt.MapFrom(scr => scr.ContactDetails.City))
				.ForMember(dto => dto.PostalCode, opt => opt.MapFrom(scr => scr.ContactDetails.PostalCode))
				.ForMember(dto => dto.Street, opt => opt.MapFrom(scr => scr.ContactDetails.Street))
				.ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(scr => scr.ContactDetails.PhoneNumber))
				.ForMember(dto => dto.Link, opt => opt.MapFrom(scr => scr.ContactDetails.Link));

			// DTO -> Entity (used in commands)
			CreateMap<DentalStudioDataTransferObject, Domain.Entities.DentalStudio>()
				.ForMember(e => e.ContactDetails, opt => opt.MapFrom(scr => new DentalStudioContactDetails
				{
					City = scr.City,
					PostalCode = scr.PostalCode,
					Street = scr.Street,
					PhoneNumber = scr.PhoneNumber,
					Link = scr.Link
				}));

			// Entity -> List DTO (summary for lists)
			CreateMap<Domain.Entities.DentalStudio, DentalStudioListItemDTO>()
				.ForMember(dto => dto.City, opt => opt.MapFrom(scr => scr.ContactDetails.City));

			// DTO -> Command
			CreateMap<DentalStudioDataTransferObject, EditDentalStudioCommand>();

			// Entity -> Details DTO (used in queries)
			CreateMap<Domain.Entities.DentalStudio, DentalStudioDetailsDto>()
				.ForMember(dto => dto.City, opt => opt.MapFrom(scr => scr.ContactDetails.City))
				.ForMember(dto => dto.Street, opt => opt.MapFrom(scr => scr.ContactDetails.Street))
				.ForMember(dto => dto.PostalCode, opt => opt.MapFrom(scr => scr.ContactDetails.PostalCode))
				.ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(scr => scr.ContactDetails.PhoneNumber))
				.ForMember(dto => dto.Link, opt => opt.MapFrom(scr => scr.ContactDetails.Link))
				.ForMember(dto => dto.IsEditable, opt => opt.MapFrom(scr =>
					user != null && (scr.CreatedById == user.Id || user.Roles.Contains("Moderator"))))
				.ForMember(dto => dto.Services, opt => opt.MapFrom(scr => scr.DentalStudioServices));
		}
	}
}
