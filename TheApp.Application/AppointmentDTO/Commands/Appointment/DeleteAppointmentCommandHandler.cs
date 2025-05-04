using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.AppointmentDTO.Commands.Appointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAppointmentRepository _repository;

        public DeleteAppointmentCommandHandler(IUserContext userContext, IAppointmentRepository repository)
        {
            _userContext = userContext;
            _repository = repository;
        }

        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var userContext = _userContext.GetCurrentUser();

            if (userContext == null)
            {
                throw new InvalidOperationException("Context user is not present");
            }

            if (!(userContext.IsInRole("Moderator") || userContext.Id == request.CreatedById.ToString()))
            {
                throw new InvalidOperationException("You are not authorized to delete this appointment.");
            }

            var service = await _repository.GetAppointmentById(request.Id);
            if (service == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {request.Id} does not exist.");
            }

            await _repository.Delete(request.Id);
        }
    }
}
