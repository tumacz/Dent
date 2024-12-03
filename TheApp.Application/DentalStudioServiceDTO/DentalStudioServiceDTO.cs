using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Entities;

namespace TheApp.Application.DentalStudioServiceDTO
{
    public class DentalStudioServiceDTO
    {
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
    }
}
