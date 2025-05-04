using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IDentalStudioRepository
    {
        Task Create(DentalStudio dentalStudio);
        //Task<DentalStudio?> GetByName(string name);
        Task<IEnumerable<DentalStudio>> GetAll();
        Task <DentalStudio> GetByEncodedName(string name);
        Task Commit();
    }
}
