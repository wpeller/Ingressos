using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using EFCore.BulkExtensions;
using Fgv.Acad.Financeiro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fgv.Acad.Financeiro.EntityFrameworkCore.Repositories
{
    public class FinanceiroRepository<TEntity, TPrimaryKey> : FinanceiroRepositoryBase<TEntity, TPrimaryKey>, IFinanceiroRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public FinanceiroRepository(IDbContextProvider<FinanceiroDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return GetAll().AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllIncludingAsNoTracking(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAllAsNoTracking();
        }

        public virtual List<TEntity> GetAllListAsNoTracking()
        {
            return GetAllAsNoTracking().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsNoTrackingAsync()
        {
            return Task.FromResult(GetAllListAsNoTracking());
        }

        public virtual List<TEntity> GetAllListAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllListAsNoTracking(predicate));
        }

        public virtual T QueryAsNoTracking<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAllAsNoTracking());
        }

        public virtual TEntity GetAsNoTracking(TPrimaryKey id)
        {
            var entity = FirstOrDefaultAsNoTracking(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsNoTrackingAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsNoTrackingAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity SingleAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(SingleAsNoTracking(predicate));
        }

        public virtual TEntity FirstOrDefaultAsNoTracking(TPrimaryKey id)
        {
            return GetAllAsNoTracking().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsNoTrackingAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefaultAsNoTracking(id));
        }

        public virtual TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefaultAsNoTracking(predicate));
        }

        public virtual TEntity LoadAsNoTracking(TPrimaryKey id)
        {
            return GetAsNoTracking(id);
        }

        public virtual int CountAsNoTracking()
        {
            return GetAllAsNoTracking().Count();
        }

        public virtual Task<int> CountAsNoTrackingAsync()
        {
            return Task.FromResult(CountAsNoTracking());
        }

        public virtual int CountAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().Where(predicate).Count();
        }

        public virtual Task<int> CountAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(CountAsNoTracking(predicate));
        }

        public virtual long LongCountAsNoTracking()
        {
            return GetAllAsNoTracking().LongCount();
        }

        public virtual Task<long> LongCountAsNoTrackingAsync()
        {
            return Task.FromResult(LongCountAsNoTracking());
        }

        public virtual long LongCountAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCountAsNoTracking(predicate));
        }

        public virtual Task BulkInsert(IList<TEntity> entities)
        {
            Context.BulkInsert(entities);

            return Task.CompletedTask;
        }

        public virtual Task BulkUpdate(IList<TEntity> entities)
        {
            Context.BulkUpdate(entities);

            return Task.CompletedTask;
        }

        public virtual Task BulkDelete(IList<TEntity> entities)
        {
            Context.BulkDelete(entities);

            return Task.CompletedTask;
        }
    }
}
