using Core.Models;
using MongoDB.Driver;

namespace Infrastructure.MongoDb;

public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
{
	private readonly IMongoCollection<T> _collection;

	public MongoRepository(IMongoDatabase database, string collectionName)
	{
		_ = database ?? throw new ArgumentNullException(nameof(database));
		_ = collectionName ?? throw new ArgumentNullException(nameof(collectionName));

		_collection = database.GetCollection<T>(collectionName);
	}

	public async Task CreateAsync(T entity)
	{
		await _collection.InsertOneAsync(entity);
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _collection.Find(_ => true).ToListAsync();
	}

	public async Task<T> GetByIdAsync(Guid id)
	{
		var filter = Builders<T>.Filter.Eq(x => x.Id, id);
		return await _collection.Find(filter).SingleOrDefaultAsync();
	}

	public async Task UpdateAsync(Guid id, T entity)
	{
		var filter = Builders<T>.Filter.Eq(x => x.Id, id);
		await _collection.ReplaceOneAsync(filter, entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var filter = Builders<T>.Filter.Eq(x => x.Id, id);
		await _collection.DeleteOneAsync(filter);
	}
}