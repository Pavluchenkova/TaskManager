using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TasksManager.Model;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Models
{
    public class TaskModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Guid _taskId;
        private string _title;
        private string _description;
        private bool _isNew;
        private bool _isModify;
        private TaskStatus _status;
        private TaskCategory _category;
        private TaskPriority _priority;

        public TaskModel()
        {
            TaskId = Guid.NewGuid();
            IsNew = false;
            IsModify = false;
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                if (String.IsNullOrEmpty(Title))
                {
                    error = "Enter the title";
                }
                return error;
            }
        }

        public Guid TaskId { get; set; }
        public DateTime CreationDate { get; set; }

        public string Title
        {
            get { return _title; }

            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public TaskCategory Category
        {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged("Category");
            }
        }

        public TaskStatus Status
        {
            get { return _status; }

            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public TaskPriority Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                RaisePropertyChanged("Priority");
            }
        }

        public bool IsNew
        {
            get { return _isNew; }

            set
            {
                _isNew = value;
                RaisePropertyChanged("IsNew");
            }
        }

        public bool IsModify
        {

            get { return _isModify; }

            set
            {
                _isModify = value;
                RaisePropertyChanged("IsModify");
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
