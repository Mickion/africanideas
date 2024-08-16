using System.Linq.Expressions;

namespace afi.university.domain.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {        
        Task<IQueryable<TEntity>> GetAllAsync(bool trackChanges);
        
        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges);

        Task<int> CreateAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

    }
}
