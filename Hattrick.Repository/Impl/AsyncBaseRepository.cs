using Hattrick.Model;
using Hattrick.Repository.Interfaces;

namespace Hattrick.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Hattrick.Model;
    using Microsoft.EntityFrameworkCore;

    public abstract class AsyncBaseRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : Entity
    {
        public virtual IUnitOfWork UnitOfWork { get; set; }

        public virtual DbSet<TEntity> Set { get; set; }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            try
            {
                await this.Set.AddAsync(entity);
                await this.UnitOfWork.Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                // TODO: logging
                throw e;
            }
        }

        public virtual async Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null)
        {
            if (func != null)
            {
                return await func(this.Set).SingleOrDefaultAsync(o => o.Id == id);
            }
            else
            {
                return await this.Set.FindAsync(id);
            }
        }

        public virtual async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null)
        {
            if (func != null)
            {
                return await Task.FromResult<IQueryable<TEntity>>(func(this.Set.Where(where)));
            }
            else
            {
                return await Task.FromResult<IQueryable<TEntity>>(this.Set.Where(where));
            }
        }

        public virtual async Task<IQueryable<TEntity>> GetAll(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null)
        {
            if (func != null)
            {
                return await Task.FromResult<IQueryable<TEntity>>(func(this.Set));
            }
            else
            {
                return await Task.FromResult<IQueryable<TEntity>>(this.Set);
            }
        }

        public virtual async Task Delete(TEntity entity)
        {
            try
            {
                this.Set.Remove(entity);
                await this.UnitOfWork.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // TODO: logging
                throw e;
            }
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
                await this.UnitOfWork.Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                // TODO: logging
                throw e;
            }
        }

        public virtual async Task UpdateAll(List<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
                }

                await this.UnitOfWork.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // TODO: logging
                throw e;
            }
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> where = null)
        {
            return where == null ? await this.Set.CountAsync() : await this.Set.CountAsync(where);
        }
    }
}