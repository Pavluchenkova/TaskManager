using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TasksManager.Model;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        private Guid taskId;
        private string title;
        private string description;
        private bool _isNew;

        public TaskModel()
        {
            TaskId = Guid.NewGuid();
            IsNew = false;
            
        }
        public Guid TaskId {
            get
            {
                return taskId;
            }
            set
            {
                taskId = value;
                RaisePropertyChanged("TaskId");
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }
        public TaskCategory Category { get; set; }
        public DateTime CreationDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsNew {

            get => _isNew;

            set
            {
                _isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
