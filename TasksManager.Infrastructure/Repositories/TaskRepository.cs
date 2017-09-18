using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using TasksManager.Infrastructure.DAL;
using TasksManager.Model;

namespace TasksManager.Infrastructure.Repository
{
    public class TaskRepository : BaseRepository<Task, TaskContext>
    {
        public TaskRepository() : base()
        {
        }
        public override void Delete(Task entity)
        {
            var task = GetById(entity.TaskId);
            DbContext.Set<Task>().Remove(task);
            DbContext.SaveChanges();
        }
        public override void Add(Task entity)
        {
            DbContext.Set<Task>().Add(entity);
            DbContext.SaveChanges();
        }

        public Task GetById(Guid id)
        {
            return GetBy(e => e.TaskId == id);
        }
        public override void Update(Task task)
        {
            DbContext.Entry(task).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

    }
}
