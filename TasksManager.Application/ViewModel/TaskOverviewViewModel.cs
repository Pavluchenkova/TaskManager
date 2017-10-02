using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TasksManager.Application.Extensions;
using TasksManager.Application.Models;
using System.Windows.Input;
using TasksManager.Application.Services;
using TasksManager.Application.Utility;
using TasksManager.Model.Entities;

namespace TasksManager.Application.ViewModel
{
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TaskModel newTask;
        private TaskModel selectedTask;
        private TaskDataService taskDataService;

        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskModel> _tasksInProgress;
        private ObservableCollection<TaskModel> _tasksDone;
        private ObservableCollection<TaskModel> _tasksToDo;
        private TaskStatus _filterName;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand DetailCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public CustomCommand SaveTaskCommand { get; set; }
        public CustomCommand MakeDoneCommand { get; set; }
        public CustomCommand ChooseInProgressListCommand { get; set; }
        public CustomCommand ChooseDoneListCommand { get; set; }
        public CustomCommand ChooseToDoListCommand { get; set; }
        public CustomCommand CancelCommand { get; set; }
        public ObservableCollection<TaskModel> Tasks
        {
            get
            {
                switch (FilterName)
                {
                    case TaskStatus.ToDo:
                        return TasksToDo;
                    case TaskStatus.InProgress:
                        return TasksInProgress;
                    case TaskStatus.Done:
                        return TasksDone;
                    default:
                        return TasksToDo;
                }

            }
            set
            {
                _tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }

        public ObservableCollection<TaskModel> TasksInProgress
        {
            get
            {
                return _tasksInProgress;
            }
            set
            {
                _tasksInProgress = value;
                RaisePropertyChanged("TasksInProgress");
            }
        }
        public ObservableCollection<TaskModel> TasksDone
        {
            get
            {
                return _tasksDone;
            }
            set
            {
                _tasksDone = value;
                RaisePropertyChanged("TasksDone");
            }
        }
        public ObservableCollection<TaskModel> TasksToDo
        {
            get
            {
                return _tasksToDo;
            }
            set
            {
                _tasksToDo = value;
                RaisePropertyChanged("TasksTodo");
            }
        }
        public TaskModel SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                selectedTask = value;
                RaisePropertyChanged("SelectedTask");
            }
        }
        public TaskStatus FilterName
        {
            get
            {
                return _filterName;
            }
            set
            {
                _filterName = value;
                RaisePropertyChanged("FilterName");
                RaisePropertyChanged("Tasks");
            }
        }

        public TaskModel NewTask
        {
            get
            {
                return newTask;
            }
            set
            {
                newTask = value;
                RaisePropertyChanged("NewTask");
            }
        }

        public IEnumerable<TaskStatus> TaskStatuses
        {
            get
            {
                return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>();
            }
        }
        public IEnumerable<TaskPriority> TaskPriorities
        {
            get
            {
                return Enum.GetValues(typeof(TaskPriority)).Cast<TaskPriority>();
            }
        }
        public IEnumerable<TaskCategory> TaskCategories
        {
            get
            {
                return Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>();
            }
        }
        public IEnumerable<TaskCategory> SelectTaskCategory
        {
            get
            {
                return Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>();
            }
        }

        public CustomCommand ClearTextCommand { get; private set; }

        public TaskOverviewViewModel()
        {
            taskDataService = new TaskDataService();
            LoadData();
            LoadCommands();
        }

        private void LoadCommands()
        {
            AddTaskCommand = new CustomCommand(AddTask, CanAddTask);
            DeleteTaskCommand = new CustomCommand(DeleteTask, CanDeleteTask);
            EditTaskCommand = new CustomCommand(EditTask, CanEditTask);
            SaveTaskCommand = new CustomCommand(SaveTask, CanSaveTask);
            MakeDoneCommand = new CustomCommand(MakeDone, CanMakeDone);
            ChooseInProgressListCommand = new CustomCommand(ChooseInProgress, CanChooseInProgress);
            ChooseDoneListCommand = new CustomCommand(ChooseDone, CanChooseDone);
            ChooseToDoListCommand = new CustomCommand(ChooseToDo, CanChooseToDo);
            CancelCommand = new CustomCommand(Cancel, CanCancel);
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void Cancel(object obj)
        {
            var task = obj as TaskModel;
            if (task.IsNew == true)
            {
                Tasks.Remove(task);
                NewTask = null;
            }
            if (task.IsModify == true)
            {
                Tasks.Remove(task);
                task = taskDataService.GetById(task.TaskId);
                Tasks.Add(task);
            }
        }

        private void ChooseInProgress(object obj)
        {
            FilterName = TaskStatus.InProgress;
        }
        private bool CanChooseInProgress(object obj)
        {
            return true;
        }

        private void ChooseToDo(object obj)
        {
            FilterName = TaskStatus.ToDo;
        }

        private bool CanChooseToDo(object obj)
        {
            return true;
        }

        private void ChooseDone(object obj)
        {
            FilterName = TaskStatus.Done;
        }
        private bool CanChooseDone(object obj)
        {
            return true;
        }
        private bool CanMakeDone(object obj)
        {
            return true;
        }

        private void MakeDone(object obj)
        {
            var task = obj as TaskModel;
            task.Status = TaskStatus.Done;
            taskDataService.Update(task);

            TasksDone.Add(task);
            TasksInProgress.Remove(task);
            Tasks.Remove(task);
        }
        private void AddTask(object obj)
        {
            NewTask = new TaskModel();
            NewTask.IsNew = true;
            Tasks.Add(NewTask);
        }

        private bool CanAddTask(object obj)
        {
            if (NewTask != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EditTask(object obj)
        {
            SelectedTask = obj as TaskModel; ;

            foreach (var task in Tasks)
            {
                if (task.IsModify == true)
                {
                    task.IsModify = false;
                }
            }
            SelectedTask.IsModify = true;
        }
        private bool CanEditTask(object obj)
        {
            return true;
        }

        private void SaveTask(object obj)
        {
            TaskModel task = obj as TaskModel;

            if (task.IsNew == true)
            {
                taskDataService.Add(task);
                task.IsNew = false;
                NewTask = null;
            }
            else
            {
                taskDataService.Update(task);
                task.IsModify = false;
            }

            var ind = Tasks.IndexOf(task);

            Tasks.RemoveAt(ind);
            if (task.Status == TaskStatus.ToDo)
            {
                TasksToDo.Add(task);
            }
            else if (task.Status == TaskStatus.InProgress)
            {
                TasksInProgress.Add(task);
            }
            else
            {
                TasksDone.Add(task);
            }
        }
        private bool CanSaveTask(object obj)
        {
            return true;
        }

        private void DeleteTask(object obj)
        {
            TaskModel task = obj as TaskModel;
            if (task != null)
            {
                taskDataService.Delete(task);
            }
            Tasks.Remove(task);
        }

        private bool CanDeleteTask(object obj)
        {
            return true;
        }

        private void LoadData()
        {
            TasksInProgress = taskDataService.GetAllInProgress().ToObservableCollection();
            TasksDone = taskDataService.GetAllDone().ToObservableCollection();
            TasksToDo = taskDataService.GetAllToDo().ToObservableCollection();
            Tasks = TasksToDo;          
        }
    }
}
