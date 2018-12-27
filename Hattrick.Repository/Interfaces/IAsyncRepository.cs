namespace Hattrick.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null);

        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null);

        Task<IQueryable<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null);

        Task Delete(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<int> Count(Expression<Func<TEntity, bool>> where = null);

        Task UpdateAll(List<TEntity> entities);
    }

}