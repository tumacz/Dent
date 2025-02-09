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

        public async Task<IEnumerable<DentalStudioService>> GetAll() => await _dbContext.DentalStudioServices.ToListAsync();

        public async Task<IEnumerable<DentalStudioService>> GetAllByEncodedName(string encodedName) => await _dbContext.DentalStudioServices.Where(c => c.DentalStudio.EncodedName == encodedName).ToListAsync();

        public async Task DeleteDentalStudioService(int id)
        {
            var serviceToDelete = await _dbContext.DentalStudioServices.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (serviceToDelete != null)
            {
                _dbContext.Remove(serviceToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
