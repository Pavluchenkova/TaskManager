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
            var task = convertor.ConvertToTask(entity);
            repository.Delete(task);
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
    }

}
