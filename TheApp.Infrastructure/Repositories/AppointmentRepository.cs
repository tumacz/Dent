using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Persistence;

namespace TheApp.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly TheAppDbContext _dbContext;

        public AppointmentRepository(TheAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Appointment appointment)
        {
            _dbContext.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAppointmentForDentalStudio(int dentalStudioId, DateTime fromDate)
        {
            return await _dbContext.Appointments
                .Include(c => c.DentalStudioService)
                .Where(c => c.DentalStudioService.DentalStudioId == dentalStudioId && c.StartTime >= fromDate)
                .ToListAsync();
        }
    }
}
