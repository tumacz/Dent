using AutoMapper;
using TheApp.Domain.Entities;
using TheApp.Application.Appointment_CQRS.Queries;
using TheApp.Application.Appointment_CQRS.Commands;

public class AppointmentMappingProfile : Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<Appointment, AppointmentDataTransferObject>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.DentalStudioService.Description))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.StartTime.Add(src.DentalStudioService.Duration ?? TimeSpan.Zero)));
		CreateMap<Appointment, AppointmentDetailsDto>()
				.ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.DentalStudioService.DentalService.Name))
				.ForMember(dest => dest.StudioEncodedName, opt => opt.MapFrom(src => src.DentalStudioService.DentalStudio.EncodedName))
				.ForMember(dest => dest.IsEditable, opt => opt.Ignore());
	}
}