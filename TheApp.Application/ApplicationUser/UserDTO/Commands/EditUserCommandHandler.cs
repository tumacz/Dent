using MediatR;
using System.Security.Principal;
using System.Text.Json;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ApplicationUser.UserDTO.Commands
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
    {
        private readonly IAppUserRepository _repository;
        private readonly IUserContext _userContext;

        public EditUserCommandHandler(IAppUserRepository repository, IUserContext userContext) 
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null)
            {
                throw new UnauthorizedAccessException("Not authenticated user!");
            }

            var userToEdit = await _repository.GetUserById(request.Id);
            var roles = JsonSerializer.Deserialize<List<string>>(request.Roles[0]);

            bool adminChanges = (currentUser.Id == request.Id && !roles.Contains("Administrator"));

            if(adminChanges)
            {
                throw new UnauthorizedAccessException("You cannot remove the Administrator role.");
            }

            await _repository.CommitEmail(userToEdit, request.Email);
            if (roles != null) 
            {
                await _repository.CommitRoles(userToEdit, roles);
            }
        }
    }
}