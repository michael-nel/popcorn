using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        IQueryable<TEntity> ListBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetAndOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> by, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetBy(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includeProperties);

        bool Exists(Func<TEntity, bool> where);

        IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetOrderFor<TKey>(Expression<Func<TEntity, TKey>> by, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetById(TId id, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void AddList(IEnumerable<TEntity> entities);
    }
}
