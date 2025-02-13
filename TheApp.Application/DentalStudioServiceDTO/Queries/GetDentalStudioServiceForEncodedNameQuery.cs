using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Queries
{
    public class GetDentalStudioServiceForEncodedNameQuery : IRequest<IEnumerable<DentalStudioServiceDTO>>
    {
        public string EncodedName { get; set; }

        public GetDentalStudioServiceForEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
