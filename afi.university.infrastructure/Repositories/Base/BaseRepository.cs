using Microsoft.EntityFrameworkCore;
using afi.university.domain.Repositories.Base;
using System.Linq.Expressions;

namespace afi.university.infrastructure.Repositories.Base
{
    internal abstract class BaseRepository<TEntity, TDbContext> : IRepositoryBase<TEntity> 
        where TEntity : class
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);  
            // TODO: UnitOfWork
            return await _dbContext.SaveChangesAsync();            
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return await _dbContext.SaveChangesAsync();
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

        public abstract Task<TEntity> GetByIdAsync(int id, bool trackChanges);

    }
}
