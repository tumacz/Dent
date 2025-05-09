using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.ServiceTag_CQRS.Commands.CreateServiceTag
{
    public class CreateServiceTagCommandHandler : IRequestHandler<CreateServiceTagCommand, int>
    {
        private readonly IServiceTagRepository _repository;
        private readonly IUserContext _userContext;

        public CreateServiceTagCommandHandler(IServiceTagRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<int> Handle(CreateServiceTagCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            if (!currentUser.IsInRole("Administrator"))
                throw new UnauthorizedAccessException("Only administrators can create service tags.");

            var tag = new ServiceTag
            {
                Name = request.Name
            };

            await _repository.Create(tag);

            return tag.Id;
        }
    }
}