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
        private bool _isModify;

        public TaskModel()
        {
            TaskId = Guid.NewGuid();
            IsNew = false;
            IsModify = false;

        }
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime CreationDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsNew { get; set; }
        public bool IsModify
        {

            get
            {
                return _isModify;
            }

            set
            {
                _isModify = value;
                RaisePropertyChanged("IsModify");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
