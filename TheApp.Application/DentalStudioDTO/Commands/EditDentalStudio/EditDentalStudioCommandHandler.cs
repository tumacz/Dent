using MediatR;
using TheApp.Application.ApplicationUser;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DataTransferObjects.Commands.EditDentalStudio
{
    public class EditDentalStudioCommandHandler : IRequestHandler<EditDentalStudioCommand>
    {
        private readonly IDentalStudioRepository _repository;
        private readonly IUserContext _userContext;

        public EditDentalStudioCommandHandler(IDentalStudioRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(EditDentalStudioCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            var dentalStudio = await _repository.GetByEncodedName(request.EncodedName!);
            
            var isEditable = user != null && dentalStudio.CreatedById == user.Id || user != null && user.IsInRole("Moderator");
            if (!isEditable)
            {
                return;
            }

            dentalStudio.ContactDetails.PhoneNumber = request.PhoneNumber;
            dentalStudio.ContactDetails.Link = request.Link;
            dentalStudio.ContactDetails.City = request.City;
            dentalStudio.ContactDetails.PostalCode = request.PostalCode;
            dentalStudio.ContactDetails.Street = request.Street;
            dentalStudio.Description = request.Description;
            await _repository.Commit();
        }
    }
}
