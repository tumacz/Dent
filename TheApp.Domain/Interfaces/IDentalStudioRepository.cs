using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IDentalStudioRepository
    {
        Task Commit();

        Task Create(DentalStudio dentalStudio);

        Task<IEnumerable<DentalStudio>> GetAll();

        Task <DentalStudio?> GetByEncodedName(string name);

        Task<DentalStudio?> GetByOwnerId(string userId);
    }
}
