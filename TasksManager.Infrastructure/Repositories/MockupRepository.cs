using System;
using System.Collections.Generic;
using System.Linq;
using TasksManager.Model;

namespace TasksManager.Infrastructure.Repositories
{
    public class MockupRepository
    {
        private static List<Task> tasks;

        public MockupRepository()
        {

        }
        public Task GetTask()
        {
            if (tasks == null)
                LoadTasks();
            return tasks.FirstOrDefault();
        }

        public Task GetById(Guid taskId)
        {
            LoadTasks();
            return tasks.Where(c => c.TaskId == taskId).FirstOrDefault();
        }

        public List<Task> GetAll()
        {
            if (tasks == null)
                LoadTasks();
            return tasks;
        }

        public Task GetTaskById(Task task)
        {
            //if (tasks == null)
            //     LoadTasks();
            return tasks.Where(c => c.TaskId == task.TaskId).FirstOrDefault();
        }

        public void Delete(Task task)
        {
            Task taskToUpdate = tasks.Where(c => c.TaskId == task.TaskId).FirstOrDefault();
            tasks.Remove(taskToUpdate);
        }

        public void Update(Task task)
        {
            Task taskToUpdate = tasks.Where(c => c.TaskId == task.TaskId).FirstOrDefault();

            taskToUpdate.Title = task.Title;
            taskToUpdate.Status = task.Status;
            taskToUpdate.Priority = task.Priority;
            taskToUpdate.Category = task.Category;
            taskToUpdate.CreationDate = task.CreationDate;
            taskToUpdate.Description = task.Description;

        }
        public void Add(Task entity)
        {
            tasks.Add(entity);
        }

        private void LoadTasks()
        {
            tasks = new List<Task>()
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
                   Priority = Model.TaskPriority.High

               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="Code review",
                   Description = "new",
                   Category = Model.TaskCategory.Work,
                   CreationDate = new DateTime(2017,01,08),
                   Status = Model.Entities.TaskStatus.ToDo,
                   Priority = Model.TaskPriority.High

               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="MVVM",
                   Description = "Practice MVVM pattern",
                   Category = Model.TaskCategory.Study,
                   CreationDate = new DateTime(2017,01,08),
                   Status = Model.Entities.TaskStatus.InProgress,
                   Priority = Model.TaskPriority.High

               }
            };

        }

    }
}
