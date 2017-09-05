using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksManager.Model;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Models
{
    public class TaskModel
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime CreationDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
