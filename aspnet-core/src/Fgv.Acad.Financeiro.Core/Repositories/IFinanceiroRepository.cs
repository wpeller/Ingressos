using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Fgv.Acad.Financeiro.Repositories
{
    public interface IFinanceiroRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        int CountAsNoTracking();

        int CountAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsNoTrackingAsync();

        Task<int> CountAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefaultAsNoTracking(TPrimaryKey id);

        TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsNoTrackingAsync(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAllAsNoTracking();

        IQueryable<TEntity> GetAllIncludingAsNoTracking(params Expression<Func<TEntity, object>>[] propertySelectors);

        List<TEntity> GetAllListAsNoTracking();

        List<TEntity> GetAllListAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsNoTrackingAsync();

        Task<List<TEntity>> GetAllListAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetAsNoTracking(TPrimaryKey id);

        Task<TEntity> GetAsNoTrackingAsync(TPrimaryKey id);

        TEntity LoadAsNoTracking(TPrimaryKey id);

        long LongCountAsNoTracking();

        long LongCountAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsNoTrackingAsync();

        Task<long> LongCountAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        T QueryAsNoTracking<T>(Func<IQueryable<TEntity>, T> queryMethod);

        TEntity SingleAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        Task BulkInsert(IList<TEntity> entities);

        Task BulkUpdate(IList<TEntity> entities);

        Task BulkDelete(IList<TEntity> entities);
    }

    public interface IFinanceiroRepository<TEntity> : IFinanceiroRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}