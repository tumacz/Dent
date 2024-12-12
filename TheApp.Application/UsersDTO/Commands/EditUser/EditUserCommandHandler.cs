using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.UsersDTO.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserIdentityManagerRepository _repository;
        private readonly IUserContext _userContext;

        public EditUserCommandHandler(IMapper mapper, IUserIdentityManagerRepository repository, IUserContext userContext)
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var userContext = _userContext.GetCurrentUser();
            bool AdminRole = false;

            if (userContext != null)
            {
                AdminRole = userContext.IsInRole("Administrator");
            }

            //find by id
            var user = await _repository.GetUserByName(request.Name);

            if (user == null)
            {
                throw new InvalidOperationException($"No user: {request.Name} has not beed found");
            }

            if (userContext == null)
            {
                throw new InvalidOperationException("You cannot edit while you are not authenticated nor authorized.");
            }

            if (!AdminRole)
            {
                throw new InvalidOperationException("You cannot edit while you are not authorized.");
            }

            if (userContext.Id == request.Id && userContext.IsInRole("Administrator") && !request.Roles.Contains("Administrator"))
            {
                throw new InvalidOperationException("You cannot remove your own Administrator role.");
            }

            user.Roles = request.Roles;
            user.Email = request.Email;
            await _repository.Commit(user);
        }
    }
}
