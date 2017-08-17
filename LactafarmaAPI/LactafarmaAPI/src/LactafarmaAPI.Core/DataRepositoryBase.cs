using LactafarmaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace LactafarmaAPI.Core
{
    public abstract class DataRepositoryBase<TEntity, TContext, TUser> : IDataRepository<TEntity>
        where TEntity : class, IIdentifiableEntity, new()
        where TContext : DbContext
        where TUser : class, new()
    {
        public TContext EntityContext;
        public TUser User => (TUser)_cache.Get("User");
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMemoryCache _cache;

        protected DataRepositoryBase(TContext entityContext, IMemoryCache cache)
        {
            EntityContext = entityContext;
            _cache = cache;
            _dbSet = EntityContext.Set<TEntity>();
        }

        protected abstract Expression<Func<TEntity, bool>> IdentifierPredicate(int id);

        public virtual TEntity Add(TEntity entity)
        {
            TEntity addedEntity = AddEntity(entity);
            EntityContext.SaveChanges();
            return addedEntity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntity addedEntity = AddEntity(entity);
            await EntityContext.SaveChangesAsync();
            return addedEntity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Modified;

            TEntity existingEntity = UpdateEntity(entity);

            EntityContext.SaveChanges();
            return existingEntity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Modified;

            TEntity existingEntity = UpdateEntity(entity);

            await EntityContext.SaveChangesAsync();
            return existingEntity;
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual void Remove(int id)
        {
            TEntity entity = GetEntity(id);
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }        
        
        public virtual IEnumerable<TEntity> FindAll()
        {
            return (GetEntities()).Where(e => e.EntityId != 0);
        }

        public virtual TEntity FindById(int id)
        {
            return GetEntity(id);
        }

        TEntity AddEntity(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        IQueryable<TEntity> GetEntities()
        {
            return _dbSet;
        }

        TEntity GetEntity(int id)
        {
            return _dbSet.Where(IdentifierPredicate(id)).FirstOrDefault();
        }

        TEntity UpdateEntity(TEntity entity)
        {
            var q = _dbSet.Where(IdentifierPredicate(entity.EntityId));
            return q.FirstOrDefault();
        }
    }

    public abstract class DataGuidRepositoryBase<TEntity, TContext, TUser> : IDataGuidRepository<TEntity>
        where TEntity : class, IIdentifiableGuidEntity, new()
        where TContext : DbContext
        where TUser : class, new()

    {
        public TContext EntityContext;
        public TUser User;
        private readonly DbSet<TEntity> _dbSet;

        protected DataGuidRepositoryBase(TContext entityContext)
        {
            EntityContext = entityContext;
            _dbSet = EntityContext.Set<TEntity>();
        }

        protected abstract Expression<Func<TEntity, bool>> IdentifierPredicate(Guid id);

        public virtual TEntity Add(TEntity entity)
        {
            TEntity addedEntity = AddEntity(entity);
            EntityContext.SaveChanges();
            return addedEntity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntity addedEntity = AddEntity(entity);
            await EntityContext.SaveChangesAsync();
            return addedEntity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Modified;

            TEntity existingEntity = UpdateEntity(entity);

            EntityContext.SaveChanges();
            return existingEntity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Modified;

            TEntity existingEntity = UpdateEntity(entity);

            await EntityContext.SaveChangesAsync();
            return existingEntity;
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual void Remove(Guid id)
        {
            TEntity entity = GetEntity(id);
            EntityContext.Entry<TEntity>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return (GetEntities()).Where(e => e.EntityId != null);
        }

        public virtual TEntity FindById(Guid id)
        {
            return GetEntity(id);
        }

        TEntity AddEntity(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        IQueryable<TEntity> GetEntities()
        {
            return _dbSet;
        }

        TEntity GetEntity(Guid id)
        {
            return _dbSet.Where(IdentifierPredicate(id)).FirstOrDefault();
        }

        TEntity UpdateEntity(TEntity entity)
        {
            var q = _dbSet.Where(IdentifierPredicate(entity.EntityId));
            return q.FirstOrDefault();
        }
    }

}
