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
        public Task GetById(Guid id)
        {
            return GetBy(e => e.TaskId == id);
        }
    }
}
