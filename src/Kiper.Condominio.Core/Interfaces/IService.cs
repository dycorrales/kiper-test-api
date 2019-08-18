using Kiper.Condominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IService<T> where T : Entity
    {
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);

        bool Any(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll();
        T FindById(Guid id);

        bool ExistsElements();
    }
}
