using Core.Models;
using Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoServices
{
	public static IServiceCollection AddMongoDbServices<T>(this IServiceCollection services, IConfiguration configuration, string collectionName) where T : BaseEntity
	{
		ArgumentNullException.ThrowIfNull(services);
		ArgumentNullException.ThrowIfNull(configuration);
		ArgumentNullException.ThrowIfNull(collectionName);

		// MongoDB Defaults		
		BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

		services.AddSingleton<IMongoClient>(sp =>
		{
			var mongoConnectionString = configuration.GetConnectionString("MongoDB");
			var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);

			mongoClientSettings.SocketTimeout = TimeSpan.FromSeconds(60);

			return new MongoClient(mongoClientSettings);
		});

		services.AddScoped<IMongoRepository<T>>(sp =>
		{
			var mongoClient = sp.GetRequiredService<IMongoClient>();
			var databaseName = configuration["MongoDb:DatabaseName"];
			var database = mongoClient.GetDatabase(databaseName);
			return new MongoRepository<T>(database, collectionName);
		});

		return services;
	}
}