using System;
using System.Collections.Generic;
using System.Linq;
using TasksManager.Model.Entities;

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
            return tasks.Where(c => c.TaskId == task.TaskId).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            Task taskToUpdate = tasks.Where(c => c.TaskId == id).FirstOrDefault();
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
                   Category =  TaskCategory.Finance,
                   CreationDate = new DateTime(2017,01,08),
                   Status =  TaskStatus.Done,
                   Priority =  TaskPriority.Medium
               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="EPM",
                   Description = "Send EPM",
                   Category =  TaskCategory.Work,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.InProgress,
                   Priority =  TaskPriority.High

               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="Code review",
                   Description = "new",
                   Category =  TaskCategory.Work,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.ToDo,
                   Priority =  TaskPriority.High

               },
               new Task()
               {
                   TaskId = Guid.NewGuid(),
                   Title ="MVVM",
                   Description = "Practice MVVM pattern",
                   Category =  TaskCategory.Study,
                   CreationDate = new DateTime(2017,01,08),
                   Status = TaskStatus.InProgress,
                   Priority =  TaskPriority.High

               }
            };
        }

        public void Update(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
