using System;
using System.Data.Entity;
using TasksManager.Model.Entities;

namespace TasksManager.Infrastructure.DAL
{
    public class TaskContextInitialiser : CreateDatabaseIfNotExists<TaskContext>
    {
        protected override void Seed(TaskContext context)
        {
            Task[] tasks = new Task[]
            {
               new Task ()
               {
                   Id = Guid.NewGuid(),
                   Title ="Taxes",
                   Description ="Pay the taxes",
                   Category = TaskCategory.Finance,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.Done,
                   Priority = TaskPriority.Medium
               },
               new Task ()
               {
                   Id = Guid.NewGuid(),
                   Title ="Code review",
                   Description ="Task manager app",
                   Category =  TaskCategory.Finance,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.ToDo,
                   Priority =  TaskPriority.High
               },
               new Task()
               {
                   Id = Guid.NewGuid(),
                   Title ="EPM",
                   Description = "Send EPM",
                   Category = TaskCategory.Work,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.InProgress,
                   Priority = TaskPriority.High

               },
               new Task()
               {
                   Id = Guid.NewGuid(),
                   Title ="MVVM",
                   Description = "Practice MVVM pattern",
                   Category =  TaskCategory.Study,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.InProgress,
                   Priority = TaskPriority.High
               }
            };

            context.Tasks.AddRange(tasks);
        }
    }
}
