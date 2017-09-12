using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TasksManager.Application.Models;
using TasksManager.Application.Services;
using TasksManager.Infrastructure.Repository;
using TasksManager.Model;
using TasksManager.Model.Repositories;

namespace TasksManager.Application
{
    public class TaskDataService : IDataServise<TaskModel>
    {
        TaskRepository repository = new TaskRepository();
        Convertor convertor = new Convertor();
        public void Add(TaskModel entity)
        {
            var task = convertor.ConvertToTask(entity);
            repository.Add(task);
        }

        public void Delete(TaskModel entity)
        {
            repository.Delete(e => e.TaskId == entity.TaskId);
        }

        public IEnumerable<TaskModel> GetAll()
        {
            var tasks = repository.GetAll();
            return convertor.ConvertToTaskModel(tasks);
        }

        public void Update(TaskModel entity)
        {
            var task = convertor.ConvertToTask(entity);
            repository.Update(task);
        }

        internal IEnumerable<TaskModel> GetAllInProgress()
        {
            var task = repository.GetAll();
            var taskInProgress = task.Where(e => e.Status == Model.Entities.TaskStatus.InProgress).ToList();
            return convertor.ConvertToTaskModel(taskInProgress);
        }
    }

}
