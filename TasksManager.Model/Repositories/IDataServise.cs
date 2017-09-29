using System.Collections.Generic;

namespace TasksManager.Model.Repositories
{
    public interface IDataServise<T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
