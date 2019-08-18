using Kiper.Condominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Insert(T entity);
        void Insert<U>(U entity) where U : class;
        void InsertRange(IEnumerable<T> entites);
        void Update(T entity);
        void Update<U>(U entity) where U : class;
        void UpdateRange(IEnumerable<T> entites);

        void Delete(T entity);

        bool Any(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindAll();
        IEnumerable<U> FindAllByUserId<U>(Guid userId) where U : Entity;
        T FindById(Guid id);
        U FindById<U>(Guid id) where U : Entity;
        IEnumerable<T> FindByIds(IEnumerable<Guid> ids);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
