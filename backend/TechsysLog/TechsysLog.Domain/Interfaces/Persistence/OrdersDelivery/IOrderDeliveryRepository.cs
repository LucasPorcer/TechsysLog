using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces.Persistence.OrdersDelivery
{
    public interface IOrderDeliveryRepository : IMongoRepositoryBase<OrderDelivery>
    {
        Task<OrderDelivery> GetByOrderNumberAsync(int orderNumber);
    }
}
