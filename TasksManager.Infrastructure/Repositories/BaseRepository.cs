using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Model.Repositories;

namespace TasksManager.Infrastructure.Repository
{
    public abstract class BaseRepository<T, C> : IRepository<T> where T : class where C : DbContext, new()
    {
        private readonly C _dbContext;
        public BaseRepository()
        {
            _dbContext = new C();

        }

        public C DbContext => _dbContext;

        public abstract void Add(T entity);
        //{
        //    DbContext.Set<T>().Add(entity);
        //    DbContext.SaveChanges();
        //}

        public abstract void Delete(T entity);

        public IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>().ToList();
        }

        public T GetBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().FirstOrDefault(predicate);
        }

        public abstract void Update(T entity);
        //{
        //    DbContext.Entry(entity).State = EntityState.Modified;
        //    DbContext.SaveChanges();
        //}
    }
}
