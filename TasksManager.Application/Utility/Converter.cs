using System.Collections.Generic;
using System.Linq;
using TasksManager.Application.Models;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Utility
{
    public class Converter
    {
        public TaskModel ConvertToTaskModel(Task entity)
        {
            TaskModel taskModel = new TaskModel();

            taskModel.TaskId = entity.Id;
            taskModel.Title = entity.Title;
            taskModel.Status = (TaskStatusModel)(int)entity.Status;
            taskModel.Priority = (TaskPriorityModel)(int)entity.Priority;
            taskModel.CreationDate = entity.CreationDate;
            taskModel.FinishDate = entity.FinishDate;
            taskModel.Description = entity.Description;
            taskModel.CategoryId = entity.CategoryId;

            return taskModel;
        }

        public IEnumerable<TaskModel> ConvertToTaskModel(IEnumerable<Task> tasks)
        {
            var taskModels = tasks.Select(e => ConvertToTaskModel(e));
            return taskModels;
        }

        public Task ConvertToTask(TaskModel taskModel)
        {
            var task = new Task();

            task.Id = taskModel.TaskId;
            task.Title = taskModel.Title;
            task.Status = (TaskStatus)(int)taskModel.Status;
            task.Priority = (TaskPriority)(int)taskModel.Priority;
            task.CreationDate = taskModel.CreationDate;
            task.FinishDate = taskModel.FinishDate;
            task.Description = taskModel.Description;
            task.CategoryId = taskModel.CategoryId;
            
            return task;
        }
        public CategoryModel ConvertToCategoryModel(Category entity)
        {
            CategoryModel categoryModel = new CategoryModel();

            categoryModel.CategoryId = entity.Id;
            categoryModel.Name = entity.Name;

            return categoryModel;
        }
        public Category ConvertToCategory(CategoryModel categoryModel)
        {
            var category = new Category();

            category.Id = categoryModel.CategoryId;
            category.Name = categoryModel.Name;

            return category;
        }
        public IEnumerable<CategoryModel> ConvertToCategoryModel(IEnumerable<Category> categories)
        {
            var categoryModel = categories.Select(e => ConvertToCategoryModel(e));
            return categoryModel;
        }
    }
}

