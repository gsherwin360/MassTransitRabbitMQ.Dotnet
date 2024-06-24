using Core.Models;

namespace Infrastructure.MongoDb;

public interface IMongoRepository<T> where T : BaseEntity
{
	Task CreateAsync(T entity);
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(Guid id);
	Task UpdateAsync(Guid id, T entity);
	Task DeleteAsync(Guid id);
}
