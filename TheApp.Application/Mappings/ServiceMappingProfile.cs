using AutoMapper;
using TheApp.Application.DentalStudioServiceDTO;
using TheApp.Application.DentalStudioServiceDTO.Commands.CreateService;
using TheApp.Application.DentalStudioServiceDTO.Commands.EditService;
using TheApp.Domain.Entities;

namespace TheApp.Application.Mappings
{
	public class ServiceMappingProfile : Profile
	{
		public ServiceMappingProfile()
		{
			CreateMap<DentalStudioService, DentalStudioServiceDataTransferObject>()
				.ForMember(dto => dto.ServiceTagName, opt => opt.MapFrom(e => e.DentalService != null ? e.DentalService.Name : string.Empty))
				.ForMember(dto => dto.StudioEncodedName, opt => opt.MapFrom(e => e.DentalStudio.EncodedName))
				.ForMember(dto => dto.IsEditable, opt => opt.Ignore());

			CreateMap<CreateDentalStudioServiceCommand, DentalStudioService>()
				.ForMember(dest => dest.DentalServiceId, opt => opt.MapFrom(src => src.DentalServiceId))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
				.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
				.ForMember(dest => dest.DentalStudioId, opt => opt.Ignore())
				.ForMember(dest => dest.DentalStudio, opt => opt.Ignore())
				.ForMember(dest => dest.Appointments, opt => opt.Ignore())
				.ForMember(dest => dest.DentalService, opt => opt.Ignore());

			CreateMap<EditDentalStudioServiceCommand, DentalStudioService>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
				.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));
		}
	}
}