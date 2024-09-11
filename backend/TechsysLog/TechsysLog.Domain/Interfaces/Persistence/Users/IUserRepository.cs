using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces.Persistence.Users
{
    public interface IUserRepository : IMongoRepositoryBase<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
