using MediatR;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ApplicationUser.UserDTO.Commands
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
    {
        private readonly IAppUserRepository _repository;

        public EditUserCommandHandler(IAppUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var userToEdit = await _repository.GetUserById(request.Id);

            await _repository.CommitEmail(userToEdit, request.Email);            
            await _repository.CommitRoles(userToEdit, request.Roles);
        }
    }
}
