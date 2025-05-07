using MediatR;
using TheApp.Application.DentalStudio.DataTransferObjects;

namespace TheApp.Application.DentalStudios.Commands.CreateDentalStudio
{
    public class CreateDentalStudioCommand : DentalStudioDataTransferObject, IRequest<int>
    {
    }
}
