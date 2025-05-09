using MediatR;
using TheApp.Application.DentalStudioServiceDTO;

namespace TheApp.Application.DentalStudioService_CQRS.Queries.GetServiceForEncodedName
{
    public class GetDentalStudioServiceForEncodedNameQuery : IRequest<IEnumerable<DentalStudioServiceDataTransferObject>>
    {
        public string EncodedName { get; set; }

        public GetDentalStudioServiceForEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
