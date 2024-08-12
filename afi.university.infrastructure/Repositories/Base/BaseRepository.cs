using Microsoft.EntityFrameworkCore;
using afi.university.domain.Repositories.Base;

namespace afi.university.infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity> 
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

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
