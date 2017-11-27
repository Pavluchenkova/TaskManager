using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Infrastructure.Repositories;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Services
{
    public class CategoryService
    {
        CategoryRepository repository = new CategoryRepository();
        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll(); 
        }
        internal void Add(Category category)
        {
            repository.Add(category);
        }

        internal void Update(Category category)
        {
            repository.GetById(category.Id);
        }
        public void Delete(Category category)
        {
            repository.Delete(category.Id);
        }

        internal Category GetById(Guid? categoryId)
        {
           return repository.GetById(categoryId);
        }
    }
}
