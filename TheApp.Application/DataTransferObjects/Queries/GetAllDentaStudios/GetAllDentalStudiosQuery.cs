using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery
{
    public class GetAllDentalStudiosQuery : IRequest<IEnumerable<DentalStudioDTO>>
    {

    }
}
