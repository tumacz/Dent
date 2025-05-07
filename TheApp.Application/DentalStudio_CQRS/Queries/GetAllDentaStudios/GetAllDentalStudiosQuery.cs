using MediatR;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;

namespace TheApp.Application.DentalStudios.Queries.GetAllDentalStudios
{
    public class GetAllDentalStudiosQuery : IRequest<IEnumerable<DentalStudioListItemDTO>>
	{
	}
}