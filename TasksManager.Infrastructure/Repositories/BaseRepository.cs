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
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            T entity = GetBy(predicate);
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetBy(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
