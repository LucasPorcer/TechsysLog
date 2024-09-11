using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using TechsysLog.Domain.Interfaces.Persistence;

namespace TechsysLog.Infrastructure.Persistence.Repositories
{
    public abstract class MongoRepositoryBase<T> : IMongoRepositoryBase<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;
        private readonly IMongoClient _client;

        public MongoRepositoryBase(IMongoClient client, IConfiguration configuration, string collectionName)
        {
            var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"] ?? "Logistics");
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var idProp = properties.FirstOrDefault(x => x.Name == "Id");

            if (idProp != null)
            {
                var idValue = idProp.GetValue(entity)?.ToString();

                if (string.IsNullOrEmpty(idValue))
                {
                    var newId = ObjectId.GenerateNewId().ToString();
                    idProp.SetValue(entity, newId);
                }
            }

            await _collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }

     
        public async Task<List<T>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;

            return await _collection.Find(FilterDefinition<T>.Empty)
                                     .Skip(skip)
                                     .Limit(pageSize)
                                     .ToListAsync();
        }
       
        public async Task<long> GetTotalCountAsync()
        {
            return await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
        }
    }


}
