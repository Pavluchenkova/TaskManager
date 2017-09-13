using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksManager.Model;
using TaskModel = TasksManager.Application.Models.TaskModel;

namespace TasksManager.Application.Services
{
    public class Convertor
    {
        public Convertor() { }

        public TaskModel ConvertToTaskModel(Task task)
        {
            TaskModel taskModel = new TaskModel();

            taskModel.TaskId = task.TaskId;
            taskModel.Title = task.Title;
            taskModel.Status = task.Status;
            taskModel.Priority = task.Priority;
            taskModel.Category = task.Category;
            taskModel.CreationDate = task.CreationDate;
            taskModel.Description = task.Description;
            return taskModel;
        }
        public IEnumerable<TaskModel> ConvertToTaskModel(IEnumerable<Task> tasks)
        {
            var taskModels = tasks.Select(e => new TaskModel()
            {
                TaskId = e.TaskId,
                Title = e.Title,
                Status = e.Status,
                Priority = e.Priority,
                Category = e.Category,
                CreationDate = e.CreationDate,
                Description = e.Description,
            }).ToList();
            return taskModels;
        }

        public Task ConvertToTask(TaskModel taskModel)
        {
            var task = new Task();

            task.TaskId = taskModel.TaskId;
            task.Title = taskModel.Title;
            task.Status = taskModel.Status;
            task.Priority = taskModel.Priority;
            task.Category = taskModel.Category;
            task.CreationDate = taskModel.CreationDate;
            task.Description = taskModel.Description;
            return task;
        }
    }

}

