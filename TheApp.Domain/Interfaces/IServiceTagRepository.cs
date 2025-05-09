using TheApp.Domain.Entities;

namespace TheApp.Domain.Interfaces
{
	public interface IServiceTagRepository
	{
		Task Create(ServiceTag dentalStudioservice);
		Task<ServiceTag> GetTagById(int id);
		Task<IEnumerable<ServiceTag>> GetAllTags();
		Task Delete(int id);
		Task Update(ServiceTag tag);
	}
}
