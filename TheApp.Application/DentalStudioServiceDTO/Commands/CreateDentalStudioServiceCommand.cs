using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.DentalStudioServiceDTO.Commands
{
    public class CreateDentalStudioServiceCommand : DentalStudioServiceDTO, IRequest
    {
        public string DentalStudioEncodedName { get; set; } = default!;
    }
}
