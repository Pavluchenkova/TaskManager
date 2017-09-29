using System;
using System.Collections.Generic;
using System.Linq;
using TasksManager.Application.Models;
using TasksManager.Infrastructure.Repositories;
using TasksManager.Model.Repositories;

namespace TasksManager.Application.Services
{
    public class TaskDataService : IDataServise<TaskModel>
    {
        TaskRepository repository = new TaskRepository();          //Uncoment after finish
        //MockupRepository repository = new MockupRepository();
        Convertor convertor = new Convertor();

        public void Delete(TaskModel taskModel)
        {
           repository.Delete(taskModel.TaskId);           
        }

        public IEnumerable<TaskModel> GetAll()
        {
            var tasks = repository.GetAll();
            return convertor.ConvertToTaskModel(tasks);
        }

        internal IEnumerable<TaskModel> GetAllInProgress()
        {
            var tasks = repository.GetAll();
            var tasksInProgress = tasks.Where(e => e.Status == Model.Entities.TaskStatus.InProgress).ToList();
            return convertor.ConvertToTaskModel(tasksInProgress);
        }
        internal IEnumerable<TaskModel> GetAllToDo()
        {
            var tasks = repository.GetAll();
            var tasksToDo = tasks.Where(e => e.Status == Model.Entities.TaskStatus.ToDo).ToList();
            return convertor.ConvertToTaskModel(tasksToDo);
        }
        internal IEnumerable<TaskModel> GetAllDone()
        {
            var tasks = repository.GetAll();
            var tasksDone = tasks.Where(e => e.Status == Model.Entities.TaskStatus.Done).ToList();
            return convertor.ConvertToTaskModel(tasksDone);
        }
        internal TaskModel GetById(Guid id)
        {
            var task = repository.GetById(id);
            return convertor.ConvertToTaskModel(task);
        }

        public void Add(TaskModel taskModel) 
        {
            taskModel.IsNew = false;
            taskModel.CreationDate = DateTime.Now;
            var task = convertor.ConvertToTask(taskModel);
            repository.Add(task);
        }
        public void Update(TaskModel task)
        {
            var taskToUpdate = repository.GetById(task.TaskId);

            taskToUpdate.Title = task.Title;
            taskToUpdate.Status = task.Status;
            taskToUpdate.Priority = task.Priority;
            taskToUpdate.Category = task.Category;
            taskToUpdate.CreationDate = task.CreationDate;
            taskToUpdate.Description = task.Description;

            repository.Update(taskToUpdate.TaskId);
        }
    }

}
