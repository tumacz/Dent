using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Application.Appointment_CQRS.Queries;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForCurrentUser
{
	public class GetAppointmentsForCurrentUserQueryHandler : IRequestHandler<GetAppointmentsForCurrentUserQuery, IEnumerable<AppointmentDetailsDto>>
	{
		private readonly IAppointmentRepository _repository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;

		public GetAppointmentsForCurrentUserQueryHandler(IAppointmentRepository repository, IMapper mapper, IUserContext userContext)
		{
			_repository = repository;
			_mapper = mapper;
			_userContext = userContext;
		}

		public async Task<IEnumerable<AppointmentDetailsDto>> Handle(GetAppointmentsForCurrentUserQuery request, CancellationToken cancellationToken)
		{
			var user = _userContext.GetCurrentUser();
			if (user == null)
				throw new UnauthorizedAccessException();

			var appointments = await _repository.GetAppointmentsByClientId(user.Id);
			var dtos = _mapper.Map<List<AppointmentDetailsDto>>(appointments);

			foreach (var dto in dtos)
			{
				dto.IsEditable = true;
			}

			return dtos;
		}
	}
}