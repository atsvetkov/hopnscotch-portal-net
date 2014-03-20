using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hopnscotch.Portal.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);
    }
}
