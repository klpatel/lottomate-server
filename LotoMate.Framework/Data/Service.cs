using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LotoMate.Framework
{
    public abstract class Service<TEntity , TDbContext> : IService<TEntity, TDbContext>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly IRepository<TEntity, TDbContext> Repository;

        protected Service(IRepository<TEntity, TDbContext> repository)
            => Repository = repository;

        public virtual void Add(TEntity entity) 
            => Repository.Add(entity);
        public virtual void AddRange(IEnumerable<TEntity> entity)
            => Repository.AddRange(entity);
        public virtual void Attach(TEntity item)
            => Repository.Attach(item);

        public virtual void Delete(TEntity item)
            => Repository.Delete(item);

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default) 
            => await Repository.DeleteAsync(keyValues, cancellationToken);

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await Repository.DeleteAsync(keyValue, cancellationToken);

        public virtual void Detach(TEntity item)
            => Repository.Detach(item);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default)
            => await Repository.ExistsAsync(keyValues, cancellationToken);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) 
            => await Repository.ExistsAsync(keyValue, cancellationToken);

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default) 
            => await Repository.FindAsync(keyValues, cancellationToken);

        public virtual async Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) 
            => await Repository.FindAsync(keyValue, cancellationToken);

        public virtual void Insert(TEntity item)
            => Repository.Insert(item);

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default) 
            => await Repository.LoadPropertyAsync(item, property, cancellationToken);

        //public virtual IQuery<TEntity> Query()
        //    => Repository.Query();

        public virtual IQueryable<TEntity> Queryable() 
            => Repository.Queryable();

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters) 
            => Repository.QueryableSql(sql, parameters);

        //public virtual async Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken = default)
        //    => await Repository.Query().SelectAsync(cancellationToken);

        //public virtual async Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object[] parameters, CancellationToken cancellationToken = default) 
        //    => await Repository.Query().SelectSqlAsync(sql, parameters, cancellationToken);

        public virtual void Update(TEntity item) 
            => Repository.Update(item);
                
        public Task<TEntity> AddAndSave(TEntity entity)
        {
            //should be impletemented here or should be part of seprate interface and remove from here if not needed
            throw new NotImplementedException();
        }
    }
}