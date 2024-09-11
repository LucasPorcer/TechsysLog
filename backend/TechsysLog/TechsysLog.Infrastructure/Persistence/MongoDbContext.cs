using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration, IMongoClient client)
        {
            _database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"] ?? "Logistics");
        }
        
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<OrderDelivery> OrderDeliveries => _database.GetCollection<OrderDelivery>("OrderDeliveries");
    }

}
