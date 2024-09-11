using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Users;

namespace TechsysLog.Infrastructure.Persistence.Repositories.Users
{
    public class UserRepository : MongoRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoClient client, IConfiguration configuration)
            : base(client,configuration, "Users")
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq("Email", email),
                Builders<User>.Filter.Eq("Password", password)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

    }
}
