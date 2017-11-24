using System;
using System.ComponentModel;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        private Guid _taskId;
        private string _title;
        private string _description;
        private bool _isNew;
        private bool _isModify;
        private TaskStatus _status;
        private Category _category;
        private TaskPriority _priority;
        private DateTime? _finishDate;
        private Guid? _taskCategoryId;

        public TaskModel()
        {
            TaskId = Guid.NewGuid();
            IsNew = false;
            IsModify = false;
        }

        public Guid TaskId { get; set; }
        public DateTime CreationDate { get; set; }

        public string Title
        {
            get { return _title; }

            set
            {
                SetValue(ref _title, value);
            }
        }
        public DateTime? FinishDate 
        {
            get { return _finishDate; }

            set
            {
                SetValue(ref _finishDate, value);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetValue(ref _description,value);

            }
        }

        public Category Category
        {
            get { return _category; }
            set
            {
                SetValue(ref _category, value);
            }
        }
        public Guid? CategoryId
        {
            get { return _taskCategoryId; }
            set
            {
                SetValue(ref _taskCategoryId, value);
            }
        }

        public TaskStatus Status
        {
            get { return _status; }

            set
            {
                SetValue(ref _status, value);
            }
        }

        public TaskPriority Priority
        {
            get { return _priority; }
            set
            {
                SetValue(ref _priority, value);
            }
        }

        public bool IsNew
        {
            get { return _isNew; }

            set
            {
                SetValue(ref _isNew, value);
            }
        }

        public bool IsModify
        {

            get { return _isModify; }

            set
            {
                SetValue(ref _isModify, value);
            }
        }

        private void SetValue<T>(ref T variable, T value, string propertyName = null)
        {
            variable = value;
            RaisePropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
