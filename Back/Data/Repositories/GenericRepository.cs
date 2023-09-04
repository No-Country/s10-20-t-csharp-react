using Microsoft.EntityFrameworkCore;
using quejapp.Data;
using s10.Back.Data.IRepositories;
using System.Linq.Expressions;

namespace s10.Back.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context;
        }
        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T? Get(int id)
        {
            return Context.Set<T>().Find(id);

        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }      
    }
}
