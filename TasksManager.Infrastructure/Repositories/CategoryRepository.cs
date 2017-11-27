using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TasksManager.Infrastructure.DAL;
using TasksManager.Model.Entities;

namespace TasksManager.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly TaskContext _dbContext;

        public TaskContext DbContext => _dbContext;

        public CategoryRepository()
        {
            _dbContext = new TaskContext();
        }

        public void Add(Category entity)
        {
            DbContext.TaskCategories.Add(entity);
            DbContext.SaveChanges();
        }

        public Category GetById(Guid? id)
        {
            return DbContext.TaskCategories.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return DbContext.TaskCategories.ToList();
        }
        public void Update(Category entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return;
            }
            DbContext.Entry(entity).State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

    }
}

