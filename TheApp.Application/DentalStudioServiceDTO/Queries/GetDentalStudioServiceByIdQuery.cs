using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceByIdQuery : IRequest<DentalStudioServiceDTO>
    {
        public int Id { get; set; }

        public GetDentalStudioServiceByIdQuery(int id)
        {
            Id = id;
        }
    }
}