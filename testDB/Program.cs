using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Infrastructure.DAL;
using TasksManager.Infrastructure.Repository;
using Task = TasksManager.Model.Task;

namespace testDB
{
    class Program
    {
        static void Main(string[] args)
        {
                TaskRepository repo = new TaskRepository();
                Task t1 = new Task()
                {
                    TaskId = Guid.NewGuid(),
                    Title = "Taxes",
                    Description = "Pay the taxes",
                    Category = TasksManager.Model.TaskCategory.Finance,
                    CreationDate = new DateTime(2017, 01, 08),
                    Status = TasksManager.Model.Entities.TaskStatus.Done,
                    Priority = TasksManager.Model.TaskPriority.High
                };
                var res = repo.GetBy(e => e.Status == TasksManager.Model.Entities.TaskStatus.InProgress);
            //repo.Add(t1);
            TaskContext tc = new TaskContext();            
            Console.WriteLine("Задач: {0}", res.Description);  
            Console.Read();
        }
    }
}
