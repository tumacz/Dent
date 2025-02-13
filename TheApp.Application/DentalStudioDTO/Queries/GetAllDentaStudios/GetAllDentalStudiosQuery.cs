using MediatR;

namespace TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery
{
    public class GetAllDentalStudiosQuery : IRequest<IEnumerable<DentalStudioDTO>>
    {

    }
}
