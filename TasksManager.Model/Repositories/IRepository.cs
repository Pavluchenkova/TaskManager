using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Model.Repositories
{
    public interface IRepository<T>
    {
        T GetBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
