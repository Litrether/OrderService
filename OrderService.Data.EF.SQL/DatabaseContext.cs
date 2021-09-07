using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Data.Core;
using OrderService.Data.Domain.Models;

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

        public DatabaseContext(IOptions<DatabaseSettings> configuration)
        {
            _client = new MongoClient(configuration.Value.ConnectionString);
            _database = _client.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name);
    }
}
