using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Task = TasksManager.Model.Task;

namespace TasksManager.Infrastructure.DAL
{
    public class TaskContextInitialiser : DropCreateDatabaseAlways<TaskContext>
    {
        protected override void Seed(TaskContext context)
        {
            Task[] tasks = new Task[]
            {
               new Task ()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="Taxes",
                   Description ="Pay the taxes",
                   Category = Model.TaskCategory.Finance,
                   CreationDate = new DateTime(2017,01,08),
                   Status = Model.Entities.TaskStatus.Done,
                   Priority = Model.TaskPriority.Medium
               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="EPM",
                   Description = "Send EPM",
                   Category = Model.TaskCategory.Work,
                   CreationDate = new DateTime(2017,01,08),
                   Status = Model.Entities.TaskStatus.InProgress,
                   Priority =Model.TaskPriority.High

               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="MVVM",
                   Description = "Practice MVVM pattern",
                   Category = Model.TaskCategory.Study,
                   CreationDate = new DateTime(2017,01,08),
                   Status = Model.Entities.TaskStatus.InProgress,
                   Priority =Model.TaskPriority.High
               }

            };

            context.Tasks.AddRange(tasks);
        }
    }
}
