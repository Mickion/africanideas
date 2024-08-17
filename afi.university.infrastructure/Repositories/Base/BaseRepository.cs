using Microsoft.EntityFrameworkCore;
using afi.university.domain.Repositories.Base;
using System.Linq.Expressions;

namespace afi.university.infrastructure.Repositories.Base
{
    internal abstract class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity> 
        where TEntity : class
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return true;          
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
            return false;
        }

        public async Task<bool> UpdateAsync(Guid id, TEntity entity)
        {
            var updateEntity = await _dbContext.Set<TEntity>().FindAsync(id);
            if(updateEntity != null)
            {
                _dbContext.Set<TEntity>().Update(updateEntity);
                return true;
            }
            return false;
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync(bool trackChanges)
        {
            //TODO: Make async, remove await Task.CompletedTask;
            await Task.CompletedTask;
            return !trackChanges ? _dbContext.Set<TEntity>().AsNoTracking() : _dbContext.Set<TEntity>();            
        }

        public async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges)
        {
            //TODO: Make async
            await Task.CompletedTask;
            return !trackChanges ? _dbContext.Set<TEntity>().Where(expression).AsNoTracking() : _dbContext.Set<TEntity>().Where(expression);
        }

        public abstract Task<TEntity> GetByIdAsync(Guid id, bool trackChanges);

    }
}
