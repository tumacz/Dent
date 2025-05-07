using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Persistence;

namespace TheApp.Infrastructure.Repositories
{
    public class DentalStudioServiceRepository : IDentalStudioServiceRepository
    {
        private readonly TheAppDbContext _dbContext;

        public DentalStudioServiceRepository(TheAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Commit() => _dbContext.SaveChangesAsync();

        public async Task Create(DentalStudioService dentalStudioService)
        {
            _dbContext.Add(dentalStudioService);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDentalStudioService(int id)
        {
            var serviceToDelete = await _dbContext.DentalStudioServices.FirstOrDefaultAsync(s => s.Id == id);
            if (serviceToDelete != null)
            {
                _dbContext.Remove(serviceToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(DentalStudioService service)
        {
            _dbContext.DentalStudioServices.Update(service);
            await _dbContext.SaveChangesAsync();
        }

        #region GET
        public async Task<IEnumerable<DentalStudioService>> GetAllServices() =>
            await _dbContext.DentalStudioServices.ToListAsync();

        public async Task<IEnumerable<DentalStudioService>> GetAllServicesByStudioEncodedName(string encodedName) =>
            await _dbContext.DentalStudioServices
                .Where(c => c.DentalStudio.EncodedName == encodedName)
                .ToListAsync();

        public async Task<DentalStudioService?> GetServiceById(int id) =>
            await _dbContext.DentalStudioServices
                .Include(s => s.DentalStudio)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<Appointment>> GetFutureAppointmentsForService(int serviceId, DateTime fromDate) =>
            await _dbContext.Appointments
                .Where(a => a.DentalStudioServiceId == serviceId && a.StartTime >= fromDate)
                .ToListAsync();
        #endregion
    }
}
