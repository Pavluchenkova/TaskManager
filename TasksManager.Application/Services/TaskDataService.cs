using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TasksManager.Infrastructure.Repository;
using TasksManager.Model;
using TasksManager.Model.Repositories;

namespace TasksManager.Application
{
    public class TaskDataService : IDataServise<Task>
    {
        TaskRepository repository = new TaskRepository();
        public void Add(Task entity)
        {
            repository.Add(entity);
        }

        public void Delete(Task entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<Task> GetAll()
        {
            return repository.GetAll();
        }

        public Task GetBy(Expression<Func<Task, bool>> predicate)
        {
           return repository.GetBy(predicate);
        }

        public void Update(Task entity)
        {
            repository.Update(entity);
        }
    }
}
