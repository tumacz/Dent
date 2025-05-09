using AutoMapper;
using TheApp.Domain.Entities;

namespace TheApp.Application.Mappings
{
	public class ServiceTagMappingProfile : Profile
	{
		public ServiceTagMappingProfile()
		{
			CreateMap<ServiceTag, ServiceTagDTO.ServiceTagDTO>().ReverseMap(); //??? spaces
		}
	}
}