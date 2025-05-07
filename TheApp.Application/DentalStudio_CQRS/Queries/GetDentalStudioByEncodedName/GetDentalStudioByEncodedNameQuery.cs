using MediatR;
using TheApp.Application.DentalStudio.DataTransferObjects;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;

namespace TheApp.Application.DentalStudios.Queries.GetDentalStudioByEncodedName
{
    public class GetDentalStudioByEncodedNameQuery : IRequest<DentalStudioDetailsDto>
	{
		public string EncodedName { get; set; }

		public GetDentalStudioByEncodedNameQuery(string encodedName)
		{
			EncodedName = encodedName;
		}
	}
}