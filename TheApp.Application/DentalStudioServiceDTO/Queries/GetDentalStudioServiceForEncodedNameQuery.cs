using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.DataTransferObjects;

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
