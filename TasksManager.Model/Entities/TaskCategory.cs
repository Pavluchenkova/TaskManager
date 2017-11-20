using System;
using System.Collections.Generic;

namespace TasksManager.Model.Entities
{
    public class TaskCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public TaskCategory()
        {
            Tasks = new List<Task>();
        }
    }
}