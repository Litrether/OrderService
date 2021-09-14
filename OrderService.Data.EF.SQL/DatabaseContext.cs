using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace OrderService.Data.EF.SQL
{
    public interface IDatabaseContext
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }

    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public DatabaseContext(IConfiguration configuration)
        {
            var connectionData = configuration.GetSection("ConnectionMongoDb");
            _client = new MongoClient(connectionData.GetSection("OrderServiceDbContext").Value);
            _database = _client.GetDatabase(connectionData.GetSection("DatabaseName").Value);
        }

        public IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name);
    }
}
