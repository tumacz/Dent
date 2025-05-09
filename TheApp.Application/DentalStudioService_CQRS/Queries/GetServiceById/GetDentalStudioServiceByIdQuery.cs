using MediatR;
using TheApp.Application.DentalStudioServiceDTO;

namespace TheApp.Application.DentalStudioService_CQRS.Queries.GetServiceById
{
    public class GetDentalStudioServiceByIdQuery : IRequest<DentalStudioServiceDataTransferObject>
    {
        public int Id { get; set; }

        public GetDentalStudioServiceByIdQuery(int id)
        {
            Id = id;
        }
    }
}