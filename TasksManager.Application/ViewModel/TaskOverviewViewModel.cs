using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TasksManager.Model;
using TasksManager.Application.Extensions;
using TasksManager.Application.Models;
using System.Windows.Input;
using TasksManager.Application.Utility;
using TasksManager.Application.View;
using TasksManager.Application.Services;
using TasksManager.Model.Entities;
using System.Windows;

namespace TasksManager.Application.ViewModel
{
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TaskModel newTask;
        private TaskModel selectedTask;
        private DialogService dialogService = new DialogService();
        private TaskDataService taskDataService;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TaskModel> tasks;
        private ObservableCollection<TaskModel> tasksInProcess;

        public ICommand DetailCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand SaveNewTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public CustomCommand SaveTaskCommand { get; set; }
        public ObservableCollection<TaskModel> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }
        public ObservableCollection<TaskModel> TasksInProcess
        {
            get
            {
                return tasksInProcess;
            }
            set
            {
                tasksInProcess = value;
                RaisePropertyChanged("TasksInProcess");
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

        public TaskOverviewViewModel()
        {
            taskDataService = new TaskDataService();
            LoadData();
            LoadCommands();
            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }

        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
            dialogService.CloseDialog();
        }

        private void LoadCommands()
        {
            DetailCommand = new CustomCommand(ShowTaskDetail, CanShowTaskDetail);
            AddTaskCommand = new CustomCommand(AddTask, CanAddTask);
            DeleteTaskCommand = new CustomCommand(DeleteTask, CanDeleteTask);
            SaveNewTaskCommand = new CustomCommand(SaveNewTask, CanSaveNewTask);
            EditTaskCommand = new CustomCommand(EditTask, CanEditTask);
            SaveTaskCommand = new CustomCommand(SaveTask, CanSaveTask);
        }

        private bool CanEditTask(object obj)
        {
            return true;
        }

        private void EditTask(object obj)
        {
           // LoadData();
            //way to redact in the same window
            TaskModel task = obj as TaskModel;            

            if (task != null)
            {                
                int ind = Tasks.IndexOf(task);

                LoadData();

                NewTask = new TaskModel();

                NewTask.TaskId = task.TaskId;
                NewTask.Title = task.Title;
                NewTask.Status = task.Status;
                NewTask.Priority = task.Priority;
                NewTask.Category = task.Category;
                NewTask.CreationDate = task.CreationDate;
                NewTask.Description = task.Description;
                NewTask.IsNew = false;
                NewTask.IsModify = true;
                
                Tasks.RemoveAt(ind);

                Tasks.Insert(ind, NewTask);
            }
        }
        private bool CanSaveNewTask(object obj)
        {
            return true;
        }
        private void SaveNewTask(object obj)
        {
            taskDataService.Add(Tasks);
            LoadData();
        }
        private bool CanSaveTask(object obj)
        {
            return true;
        }
        private void SaveTask(object obj)
        {
            TaskModel task = obj as TaskModel;
            taskDataService.Update(task);
            LoadData();
        }
        private bool CanDeleteTask(object obj)
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
            //Tasks.Remove(selectedTask);
            //TasksInProcess.Remove(selectedTask);
            LoadData();
        }

        private void AddTask(object obj)
        {
            NewTask = new TaskModel();
            NewTask.IsNew = true;
            Tasks.Add(NewTask);
        }

        private bool CanAddTask(object obj)
        {
            return true;                                        //TODO
        }

        private void ShowTaskDetail(object obj)
        {
            Messenger.Default.Send<TaskModel>(newTask);
            dialogService.ShowDialog();
        }

        private bool CanShowTaskDetail(object obj)
        {
            return true;
        }

        private void LoadData()
        {
            Tasks = taskDataService.GetAll().ToObservableCollection();
            TasksInProcess = taskDataService.GetAllInProgress().ToObservableCollection();
        }
    }
}
