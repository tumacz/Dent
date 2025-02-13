using AutoMapper;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.AppointmentDTO.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository repository, IUserContext userContext)
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Owner"))
            {
                throw new UnauthorizedAccessException("You are not authorized to create an appointment");
            }
            var appointment = _mapper.Map<Appointment>(request);

            appointment.CreatedById = currentUser.Id;
            await _repository.Create(appointment);
        }
    }
}
