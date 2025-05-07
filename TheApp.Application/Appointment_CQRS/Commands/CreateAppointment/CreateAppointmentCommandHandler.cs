using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.AppointmentDTO.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
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

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Owner"))
            {
                throw new UnauthorizedAccessException("You are not authorized to create an appointment");
            }
            var appointment = _mapper.Map<Domain.Entities.Appointment>(request);

            appointment.ClientId = currentUser.Id;
            await _repository.Create(appointment);

            return appointment.Id;
        }
    }
}
