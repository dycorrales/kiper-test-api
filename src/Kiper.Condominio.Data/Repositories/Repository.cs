using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kiper.Condominio.Core.Entities;
using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kiper.Condominio.Infra.Data.Repositories
{
    public abstract class Repository<T> where T : Entity
    {
        protected DbSet<T> DbSet;
        protected DbContext SqlContext { get; }
        protected IUser User { get; }

        public Repository(DbContext context, IUser user)
        {
            SqlContext = context;
            DbSet = SqlContext.Set<T>();
            User = user;
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Insert<U>(U entity) where U : class
        {
            SqlContext.Set<U>().Add(entity);
        }

        public async virtual void InsertRange(IEnumerable<T> entites)
        {
            await DbSet.AddRangeAsync(entites);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Update<U>(U entity) where U : class
        {
            SqlContext.Set<U>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entites)
        {
            DbSet.UpdateRange(entites);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(entity => entity.Status != Status.Deleted).Any(predicate);
        }

        public virtual IEnumerable<T> FindAll()
        {
            return DbSet.AsNoTracking().Where(entity => entity.Status != Status.Deleted).OrderByDescending(entity => entity.CreatedAt);
        }

        public virtual IEnumerable<U> FindAllByUserId<U>(Guid userId) where U : Entity
        {
            return SqlContext.Set<U>().AsNoTracking().Where(entity => entity.CreatedBy.Equals(userId) && entity.Status != Status.Deleted);
        }

        public virtual T FindById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public virtual U FindById<U>(Guid id) where U : Entity
        {
            return SqlContext.Set<U>().AsNoTracking().FirstOrDefault(entity => entity.Id.Equals(id) && entity.Status != Status.Deleted);
        }

        public virtual IEnumerable<T> FindByIds(IEnumerable<Guid> ids)
        {
            return DbSet.AsNoTracking().Where(entity => ids.Contains(entity.Id) && entity.Status != Status.Deleted);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }
        
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }        
    }
}
