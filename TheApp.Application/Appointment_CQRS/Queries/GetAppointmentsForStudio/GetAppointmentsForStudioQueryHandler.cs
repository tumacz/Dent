using AutoMapper;
using MediatR;
using TheApp.Application.Appointment_CQRS.Queries;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForStudio
{
	public class GetAppointmentsForStudioQueryHandler : IRequestHandler<GetAppointmentsForStudioQuery, IEnumerable<AppointmentDetailsDto>>
	{
		private readonly IAppointmentRepository _repository;
		private readonly IMapper _mapper;

		public GetAppointmentsForStudioQueryHandler(IAppointmentRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<AppointmentDetailsDto>> Handle(GetAppointmentsForStudioQuery request, CancellationToken cancellationToken)
		{
			var appointments = await _repository.GetAppointmentsByStudioEncodedName(request.StudioEncodedName);
			return _mapper.Map<IEnumerable<AppointmentDetailsDto>>(appointments);
		}
	}
}
