using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Foosball.Models.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> All { get; }

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T FindById(object id);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        void Save();
    }
}