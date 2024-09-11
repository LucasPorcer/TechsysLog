using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Orders;

namespace TechsysLog.Infrastructure.Persistence.Repositories.Orders
{
    public class OrderRepository : MongoRepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IMongoClient client, IConfiguration configuration)
            : base(client,configuration, "Orders")
        {
        }
      
        public async Task<Order> GetByNumberAsync(int number)
        {
            var filter = Builders<Order>.Filter.Eq("Number", number);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }

}
