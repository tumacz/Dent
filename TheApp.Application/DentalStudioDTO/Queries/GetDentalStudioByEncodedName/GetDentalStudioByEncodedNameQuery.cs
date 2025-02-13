using MediatR;

namespace TheApp.Application.DataTransferObjects.Queries.GetDentalStudioByEncodedName
{
    public class GetDentalStudioByEncodedNameQuery : IRequest<DentalStudioDTO>
	{
		public string EncodedName { get; set; }

        public GetDentalStudioByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
