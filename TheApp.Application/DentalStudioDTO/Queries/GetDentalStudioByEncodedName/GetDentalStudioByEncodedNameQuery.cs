using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
