using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Application.Models;
using TasksManager.Application.Services;
using TasksManager.Infrastructure.Repositories;
using TasksManager.Infrastructure.Repository;
using TasksManager.Model;
using TasksManager.Model.Repositories;

namespace TasksManager.Application
{
    public class TaskDataService : IDataServise<TaskModel>
    {
        //TaskRepository repository = new TaskRepository();          //Uncoment after finish
        MockupRepository repository = new MockupRepository();   
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

        //public void Update(TaskModel entity)
        //{
        //    var task = convertor.ConvertToTask(entity);
        //    repository.Update(task);
        //}

        internal IEnumerable<TaskModel> GetAllInProgress()
        {
            var task = repository.GetAll();
            var taskInProgress = task.Where(e => e.Status == Model.Entities.TaskStatus.InProgress).ToList();
            return convertor.ConvertToTaskModel(taskInProgress);
        }
        internal IEnumerable<TaskModel> GetAllToDo()
        {
            var task = repository.GetAll();
            var taskToDo = task.Where(e => e.Status == Model.Entities.TaskStatus.ToDo).ToList();
            return convertor.ConvertToTaskModel(taskToDo);
        }
        internal IEnumerable<TaskModel> GetAllDone()
        {
            var task = repository.GetAll();
            var taskDone = task.Where(e => e.Status == Model.Entities.TaskStatus.Done).ToList();
            return convertor.ConvertToTaskModel(taskDone);
        }


        internal void Add(ObservableCollection<TaskModel> taskModels)
        {

            var tasks = taskModels.Where(e => e.IsNew).ToList();
            foreach (var item in tasks)
            {
                item.TaskId = Guid.NewGuid();
                item.IsNew = false;
                item.CreationDate = DateTime.Now;                
                var task = convertor.ConvertToTask(item);
                repository.Add(task);
            }
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

            repository.Update(taskToUpdate);
        }
    }

}
