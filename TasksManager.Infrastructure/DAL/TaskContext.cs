using System.Data.Entity;
using TasksManager.Model.Entities;

namespace TasksManager.Infrastructure.DAL
{
    public class TaskContext : DbContext
    {
        static TaskContext()
        {
            Database.SetInitializer<TaskContext>(new TaskContextInitialiser());
        }

        public TaskContext()
            : base("TaskContext")
        { }
        public DbSet<Task> Tasks { get; set; }
    }
}
