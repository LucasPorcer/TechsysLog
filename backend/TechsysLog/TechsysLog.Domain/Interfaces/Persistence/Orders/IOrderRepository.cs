using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces.Persistence.Orders
{
    public interface IOrderRepository : IMongoRepositoryBase<Order>
    {
        Task<Order> GetByNumberAsync(int number);
    }
}
