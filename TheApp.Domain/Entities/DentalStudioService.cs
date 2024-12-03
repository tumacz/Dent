using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Domain.Entities
{
    public class DentalStudioService
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;
        public int DentalStudioId { get; set; } =default!;
        public DentalStudio DentalStudio { get; set; } = default!;

    }
}
