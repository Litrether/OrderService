using Microsoft.EntityFrameworkCore;
using OrderService.Data.EF.SQL;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Data.Services.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }

    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        private OrderServiceDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        protected BaseService(OrderServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> CreateAsync(TEntity newEntity)
        {
            await dbSet.AddAsync(newEntity);
            await dbContext.SaveChangesAsync();
            return newEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity newEntity)
        {
            if (dbContext.Entry(newEntity).State == EntityState.Detached)
                dbSet.Attach(newEntity);

            dbContext.ChangeTracker.DetectChanges();
            await dbContext.SaveChangesAsync();
            return newEntity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    }
}
