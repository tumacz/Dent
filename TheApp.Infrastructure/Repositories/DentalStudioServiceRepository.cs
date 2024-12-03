using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<DentalStudioService>> GetAll() => await _dbContext.Sevices.ToListAsync();

        public async Task<IEnumerable<DentalStudioService>> GetAllByEncodedName(string encodedName) => await _dbContext.Sevices.Where(c => c.DentalStudio.EncodedName == encodedName).ToListAsync();
    }
}
