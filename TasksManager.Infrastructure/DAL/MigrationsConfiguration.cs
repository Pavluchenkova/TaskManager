using System.Data.Entity.Migrations;

namespace TasksManager.Infrastructure.DAL
{
    public class MigrationsConfiguration: DbMigrationsConfiguration<TaskContext>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
