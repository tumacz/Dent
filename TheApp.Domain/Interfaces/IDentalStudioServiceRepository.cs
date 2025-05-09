using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IDentalStudioServiceRepository
    {
        Task Create(DentalStudioService dentalStudioservice);
        Task<IEnumerable<DentalStudioService>> GetAllServices();
        Task Commit();
        Task <IEnumerable<DentalStudioService>> GetAllServicesByStudioEncodedName(string encodedName);
        Task DeleteDentalStudioService(int id);
        Task <DentalStudioService?> GetServiceById(int id);
        Task <List<Appointment>> GetFutureAppointmentsForService(int id, DateTime fromDate);
        Task Update(DentalStudioService service);
    }
}