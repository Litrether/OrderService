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

        public DatabaseContext()
        {
            _client = new MongoClient("mongodb+srv://litrether:12345Qwert@cluster0.zece3.mongodb.net/myFirstDatabase");
            _database = _client.GetDatabase("myFirstDatabase");
        }

        public IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name);
    }
}
