using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IDentalStudioServiceRepository
    {
        Task Create(DentalStudioService dentalStudioservice);
        Task<IEnumerable<DentalStudioService>> GetAll();
        Task Commit();
        Task <IEnumerable<DentalStudioService>> GetAllByEncodedName(string encodedName);
    }
}
