namespace TechsysLog.Domain.Interfaces.Persistence
{
    public interface IMongoRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
        Task<List<T>> GetPagedAsync(int pageIndex, int pageSize);
        Task<long> GetTotalCountAsync();
    }
}
