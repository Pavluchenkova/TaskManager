using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TasksManager.Model;

namespace TasksManager.Infrastructure.DAL
{
    public class TaskContext : DbContext
    {
        static TaskContext()
        {
            Database.SetInitializer<TaskContext>(new TaskContextInitialiser());
        }

        public TaskContext()
            :base("TaskContext")
            { }
        public DbSet<Task> Tasks { get; set; }
    }
}
