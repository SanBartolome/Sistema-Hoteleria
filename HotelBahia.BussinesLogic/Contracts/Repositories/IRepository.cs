using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HotelBahia.BussinesLogic.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        T Get(params object[] keyValues);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void SaveChanges();
    }
}