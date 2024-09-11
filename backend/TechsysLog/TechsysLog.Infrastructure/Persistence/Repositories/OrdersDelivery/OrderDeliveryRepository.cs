using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.OrdersDelivery;

namespace TechsysLog.Infrastructure.Persistence.Repositories.OrdersDelivery
{
    public class OrderDeliveryRepository : MongoRepositoryBase<OrderDelivery>, IOrderDeliveryRepository
    {
        public OrderDeliveryRepository(IMongoClient client, IConfiguration configuration)
            : base(client, configuration, "OrderDeliveries")
        {
        }

        public async Task<OrderDelivery> GetByOrderNumberAsync(int orderNumber)
        {
            var filter = Builders<OrderDelivery>.Filter.Eq("OrderNumber", orderNumber);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
