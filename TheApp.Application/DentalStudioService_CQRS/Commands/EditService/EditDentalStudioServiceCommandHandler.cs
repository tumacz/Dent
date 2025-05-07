using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Application.DentalStudioServiceDTO.Commands.EditService;
using TheApp.Domain.Interfaces;

public class EditDentalStudioServiceCommandHandler : IRequestHandler<EditDentalStudioServiceCommand>
{
    private readonly IDentalStudioServiceRepository _repository;
    private readonly IUserContext _userContext;

    public EditDentalStudioServiceCommandHandler(IDentalStudioServiceRepository repository, IUserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    public async Task Handle(EditDentalStudioServiceCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        if (user == null)
        {
            throw new UnauthorizedAccessException("No user context");
        }

        var serviceToEdit = await _repository.GetServiceById(request.Id);

        if (serviceToEdit == null)
        {
            throw new Exception($"Service with ID {request.Id} not found.");
        }

        bool isEditable = serviceToEdit.DentalStudio.CreatedById == user.Id || user.IsInRole("Moderator");

        if (!isEditable)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this dental service.");
        }

        serviceToEdit.Description = request.Description;
        serviceToEdit.Cost = request.Cost;
        serviceToEdit.Duration = request.Duration;

        await _repository.Update(serviceToEdit);
    }
}
