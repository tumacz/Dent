using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task Create(Appointment appointment);
        Task<List<Appointment>> GetAppointmentForDentalStudio(int dentalStudioId, DateTime fromDate);
        Task<Appointment> GetAppointmentById(int id);
        Task Delete(int id);
    }
}