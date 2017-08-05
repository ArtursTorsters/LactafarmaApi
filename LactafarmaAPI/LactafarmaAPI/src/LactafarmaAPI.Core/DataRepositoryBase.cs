using LactafarmaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LactafarmaAPI.Core
{
    public abstract class DataRepositoryBase<T, U, V> : IDataRepository<T>
        where T : class, IIdentifiableEntity, new()
        where U : DbContext
        where V : class, IIdentifiableGuidEntity, new()
    {
        public U EntityContext;
        public V User;
        private readonly DbSet<T> _dbSet;

        protected DataRepositoryBase(U entityContext, V user)
        {
            EntityContext = entityContext;
            _dbSet = EntityContext.Set<T>();
            User = user;
        }

        protected abstract Expression<Func<T, bool>> IdentifierPredicate(int id);

        public virtual T Add(T entity)
        {
            T addedEntity = AddEntity(entity);
            EntityContext.SaveChanges();
            return addedEntity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            T addedEntity = AddEntity(entity);
            await EntityContext.SaveChangesAsync();
            return addedEntity;
        }

        public virtual T Update(T entity)
        {
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Modified;

            T existingEntity = UpdateEntity(entity);

            EntityContext.SaveChanges();
            return existingEntity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Modified;

            T existingEntity = UpdateEntity(entity);

            await EntityContext.SaveChangesAsync();
            return existingEntity;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual void Remove(int id)
        {
            T entity = GetEntity(id);
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }        
        
        public virtual IEnumerable<T> FindAll()
        {
            return (GetEntities()).Where(e => e.EntityId != 0);
        }

        public virtual T FindById(int id)
        {
            return GetEntity(id);
        }

        T AddEntity(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        IQueryable<T> GetEntities()
        {
            return _dbSet;
        }

        T GetEntity(int id)
        {
            return _dbSet.Where(IdentifierPredicate(id)).FirstOrDefault();
        }

        T UpdateEntity(T entity)
        {
            var q = _dbSet.Where(IdentifierPredicate(entity.EntityId));
            return q.FirstOrDefault();
        }
    }

    public abstract class DataGuidRepositoryBase<T, U, V> : IDataGuidRepository<T>
        where T : class, IIdentifiableGuidEntity, new()
        where U : DbContext
        where V : class, IIdentifiableGuidEntity, new()

    {
        public U EntityContext;
        public V User;
        private readonly DbSet<T> _dbSet;

        protected DataGuidRepositoryBase(U entityContext, V user)
        {
            EntityContext = entityContext;
            _dbSet = EntityContext.Set<T>();
            User = user;
        }

        protected abstract Expression<Func<T, bool>> IdentifierPredicate(Guid id);

        public virtual T Add(T entity)
        {
            T addedEntity = AddEntity(entity);
            EntityContext.SaveChanges();
            return addedEntity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            T addedEntity = AddEntity(entity);
            await EntityContext.SaveChangesAsync();
            return addedEntity;
        }

        public virtual T Update(T entity)
        {
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Modified;

            T existingEntity = UpdateEntity(entity);

            EntityContext.SaveChanges();
            return existingEntity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Modified;

            T existingEntity = UpdateEntity(entity);

            await EntityContext.SaveChangesAsync();
            return existingEntity;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual void Remove(Guid id)
        {
            T entity = GetEntity(id);
            EntityContext.Entry<T>(entity)
                         .State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public virtual IEnumerable<T> FindAll()
        {
            return (GetEntities()).Where(e => e.EntityId != null);
        }

        public virtual T FindById(Guid id)
        {
            return GetEntity(id);
        }

        T AddEntity(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        IQueryable<T> GetEntities()
        {
            return _dbSet;
        }

        T GetEntity(Guid id)
        {
            return _dbSet.Where(IdentifierPredicate(id)).FirstOrDefault();
        }

        T UpdateEntity(T entity)
        {
            var q = _dbSet.Where(IdentifierPredicate(entity.EntityId));
            return q.FirstOrDefault();
        }
    }

}
