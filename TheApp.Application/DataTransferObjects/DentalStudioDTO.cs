using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Entities;

namespace TheApp.Application.DataTransferObjects
{
    public class DentalStudioDTO
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }

        public string? EncodedName { get; set; }

        public bool IsEditable { get; set; }
    }
}
