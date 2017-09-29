using System;
using System.Collections.Generic;

namespace TasksManager.Model.Repositories
{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(Guid id);
        void Delete(Guid id);
    }
}
