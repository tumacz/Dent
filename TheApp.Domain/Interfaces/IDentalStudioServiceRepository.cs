using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IDentalStudioServiceRepository
    {
        Task Create(DentalStudioService dentalStudioservice);
        //Task<IEnumerable<DentalStudioService>> GetAll();
        Task Commit();
        Task <IEnumerable<DentalStudioService>> GetAllByEncodedName(string encodedName);
        Task DeleteDentalStudioService(int id);
        Task <DentalStudioService?> GetById(int id);
        Task <List<Appointment>> GetAppointmentsForService(int id, DateTime fromDate);
    }
}
