using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OrderService.Data.Core.Attributes;
using OrderService.Data.Domain;
using OrderService.Data.EF.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Data.Services.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : KeyedEntityBase
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }

    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : KeyedEntityBase
    {
        private readonly IMongoCollection<TEntity> _collection;

        protected BaseService(IDatabaseContext context)
        {
            _collection = context.GetCollection<TEntity>(GetCollectionName());
        }

        private static string GetCollectionName()
        {
            return (typeof(TEntity)
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute)
                .CollectionName;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() =>
            await _collection.Find(o => true).ToListAsync();

        public async Task<TEntity> GetAsync(int id) =>
             await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();

        public async Task<TEntity> CreateAsync(TEntity newEntity)
        {
            await _collection.InsertOneAsync(newEntity);
            return newEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity newEntity)
        {
            await _collection.ReplaceOneAsync(o => o.Id == newEntity.Id, newEntity);
            return newEntity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
                _collection.DeleteOne(o => o.Id == id);
        }
    }
}
