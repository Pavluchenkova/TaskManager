using System.Data.Entity;
using TasksManager.Model.Entities;

namespace TasksManager.Infrastructure.DAL
{
    public class TaskContext : DbContext
    {
        static TaskContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TaskContext>());
        }

        public TaskContext()
            : base()
        { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> TaskCategories { get; set; }
    }
}
