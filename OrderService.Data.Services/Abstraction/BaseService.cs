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
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }

    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : KeyedEntityBase
    {
        private readonly IMongoCollection<TEntity> _collection;

        protected BaseService(IMongoCollection<TEntity> сollection)
        {
            _collection = сollection;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
            await _collection.Find(o => true).ToListAsync();

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken) =>
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken) =>
            await _collection.DeleteOneAsync(o => o.Id == id);
    }
}
