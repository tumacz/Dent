using AutoMapper;
using TheApp.Domain.Entities;
using TheApp.Application.AppointmentDTO;

public class AppointmentMappingProfile : Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<Appointment, AppointmentDataTransferObject>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.DentalStudioService.Description))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.StartTime.Add(src.DentalStudioService.Duration ?? TimeSpan.Zero)));
    }
}

