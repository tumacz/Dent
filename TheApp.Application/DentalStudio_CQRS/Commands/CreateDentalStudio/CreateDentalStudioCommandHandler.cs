using AutoMapper;
using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudios.Commands.CreateDentalStudio
{
	public class CreateDentalStudioCommandHandler : IRequestHandler<CreateDentalStudioCommand, int>
	{
		private readonly IDentalStudioRepository _repository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;

		public CreateDentalStudioCommandHandler(IMapper mapper, IDentalStudioRepository repository, IUserContext userContext)
		{
			_mapper = mapper;
			_repository = repository;
			_userContext = userContext;
		}

		public async Task<int> Handle(CreateDentalStudioCommand request, CancellationToken cancellationToken)
		{
			var currentUser = _userContext.GetCurrentUser();
			if (currentUser == null)
			{
				throw new UnauthorizedAccessException("You are not authorized to create a dental studio");
			}

			var dentalStudio = _mapper.Map<Domain.Entities.DentalStudio>(request);
			dentalStudio.EncodeName();
			dentalStudio.CreatedById = currentUser.Id;

			await _repository.Create(dentalStudio);

			return dentalStudio.Id;
		}
	}
}