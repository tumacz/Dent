using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Persistence;

namespace TheApp.Infrastructure.Repositories
{
    public class DentalStudioRepository : IDentalStudioRepository
    {

        private readonly TheAppDbContext _dbContext;

        public DentalStudioRepository(TheAppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task Commit() => _dbContext.SaveChangesAsync();

        public async Task Create(DentalStudio dentalStudio)
        {
            _dbContext.Add(dentalStudio);
            await _dbContext.SaveChangesAsync();
        }

		public async Task<IEnumerable<DentalStudio>> GetAll() => await _dbContext.DentalStudios.ToListAsync();

		public async Task<DentalStudio?> GetByEncodedName(string encodedName) => await _dbContext.DentalStudios.FirstAsync(s => s.EncodedName == encodedName);

        public async Task<DentalStudio?> GetByOwnerId(string userId)
        {
            return await _dbContext.DentalStudios
                .Include(s => s.ContactDetails)
                .FirstOrDefaultAsync(s => s.CreatedById == userId);
        }
    }
}