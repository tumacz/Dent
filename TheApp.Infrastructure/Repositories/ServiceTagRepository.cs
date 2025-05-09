using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Persistence;

namespace TheApp.Infrastructure.Repositories
{
	public class ServiceTagRepository : IServiceTagRepository
	{
		private readonly TheAppDbContext _db;

		public ServiceTagRepository(TheAppDbContext dbContext)
		{
			_db = dbContext;
		}

		public async Task Create(ServiceTag serviceTag)
		{
			_db.ServiceTags.Add(serviceTag);
			await _db.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var tag = await _db.ServiceTags.FirstOrDefaultAsync(c => c.Id == id);
			if (tag == null)
				throw new KeyNotFoundException($"Tag with id: {id} not found");

			_db.ServiceTags.Remove(tag);
			await _db.SaveChangesAsync();
		}

		public async Task<IEnumerable<ServiceTag>> GetAllTags()
		{
			return await _db.ServiceTags.OrderBy(t => t.Name).ToListAsync();
		}

		public async Task<ServiceTag> GetTagById(int id)
		{
			var tag = await _db.ServiceTags.FirstOrDefaultAsync(t => t.Id == id);
			if (tag == null)
				throw new KeyNotFoundException($"Tag with id: {id} not found");

			return tag;
		}

		public async Task Update(ServiceTag tag)
		{
			var existing = await _db.ServiceTags.FirstOrDefaultAsync(t => t.Id == tag.Id);
			if (existing == null)
				throw new KeyNotFoundException($"Tag with id: {tag.Id} not found");

			existing.Name = tag.Name;
			await _db.SaveChangesAsync();
		}
	}
}