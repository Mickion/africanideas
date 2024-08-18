using System.Linq.Expressions;

namespace afi.university.domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id, bool trackChanges);

        Task<IQueryable<TEntity>> GetAllAsync(bool trackChanges);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges);

        Task<bool> UpdateAsync(Guid id, TEntity entity);

        Task<bool> CreateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges);

    }
}
