using System;

namespace TasksManager.Model.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public Guid? CategoryId { get; set; }
        public TaskCategory Category { get; set; }
    }
}
