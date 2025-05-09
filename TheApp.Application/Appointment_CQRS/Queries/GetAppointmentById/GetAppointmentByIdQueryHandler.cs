using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Application.Appointment_CQRS.Queries;
using TheApp.Domain.Interfaces;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDetailsDto>
{
	private readonly IAppointmentRepository _repository;
	private readonly IMapper _mapper;
	private readonly IUserContext _userContext;

	public GetAppointmentByIdQueryHandler(IAppointmentRepository repository, IMapper mapper, IUserContext userContext)
	{
		_repository = repository;
		_mapper = mapper;
		_userContext = userContext;
	}

	public async Task<AppointmentDetailsDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
	{
		var user = _userContext.GetCurrentUser();
		var appointment = await _repository.GetAppointmentById(request.Id);

		if (appointment == null)
			throw new KeyNotFoundException($"Appointment with ID {request.Id} not found");

		var dto = _mapper.Map<AppointmentDetailsDto>(appointment);
		dto.IsEditable = user != null &&
						 (appointment.ClientId == user.Id || user.IsInRole("Moderator"));

		return dto;
	}
}