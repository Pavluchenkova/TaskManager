﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TasksManager.Infrastructure.DAL;
using TasksManager.Model.Entities;


namespace TasksManager.Infrastructure.Repositories
{
    public class TaskRepository 
    {
        private readonly TaskContext _dbContext;

        public TaskContext DbContext => _dbContext;

        public TaskRepository()
        {
            _dbContext = new TaskContext();
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

        public void Add(Task entity)
        {
            DbContext.Tasks.Add(entity);
            DbContext.SaveChanges();
        }

        public Task GetById(Guid id)
        {
            return DbContext.Tasks.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Task entity)
        {      
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public IEnumerable<Task> GetAll()
        {        
            
            return DbContext.Tasks.ToList();           
        }
    }
}
