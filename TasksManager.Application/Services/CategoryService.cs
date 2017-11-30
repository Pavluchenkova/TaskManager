using System;
using System.Collections.Generic;
using TasksManager.Application.Models;
using TasksManager.Application.Utility;
using TasksManager.Infrastructure.Repositories;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Services
{
    public class CategoryService
    {
        CategoryRepository repository = new CategoryRepository();
        Converter convertor = new Converter();

        public IEnumerable<CategoryModel> GetAll()
        {
            var categories = repository.GetAll();
            return convertor.ConvertToCategoryModel(categories);
        }
        internal void Add(CategoryModel categoryModel)
        {
            var category = convertor.ConvertToCategory(categoryModel);
            repository.Add(category);
        }
        
        internal CategoryModel GetById(Guid? categoryId)
        {
           var category = repository.GetById(categoryId);
            return convertor.ConvertToCategoryModel(category);
        }
    }
}
