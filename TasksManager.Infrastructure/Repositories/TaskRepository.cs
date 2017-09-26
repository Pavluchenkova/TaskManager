using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using TasksManager.Infrastructure.DAL;
using TasksManager.Model;
using TasksManager.Model.Repositories;

namespace TasksManager.Infrastructure.Repository
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly TaskContext _dbContext;
        public TaskRepository() 
        {
            _dbContext = new TaskContext();
        }
        public TaskContext DbContext => _dbContext;
        public void Delete(Task entity)
        {
            var task = GetById(entity.TaskId);
            DbContext.Set<Task>().Remove(task);
            DbContext.SaveChanges();
        }
        public void Add(Task entity)
        {
            DbContext.Set<Task>().Add(entity);
            DbContext.SaveChanges();
        }

        public Task GetById(Guid id)
        {
            return DbContext.Set<Task>().FirstOrDefault(e => e.TaskId == id);          
        }
        public void Update(Task task)
        {
            DbContext.Entry(task).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public IEnumerable<Task> GetAll()
        {
            return DbContext.Set<Task>().ToList();
        }
    }
}
