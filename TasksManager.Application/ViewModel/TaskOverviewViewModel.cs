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
using System.Windows.Controls;
using System.Windows.Media;

namespace TasksManager.Application.ViewModel
{
    public class MyTasks:ObservableCollection<TaskModel>
    {
        public void Update()
        {
            OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Add));
        }
}
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
         
            SelectedTask = obj as TaskModel; ;

            foreach (var task in Tasks)
            {
                if(task.IsModify == true)
                {
                    task.IsModify = false;
                }

            }
            SelectedTask.IsModify = true;
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

            //SelectedTask = obj as TaskModel;

            //if (SelectedTask != null)
            //{
            //    taskDataService.Delete(SelectedTask);
            //}
            //Tasks.Remove(SelectedTask);

            TaskModel task = obj as TaskModel;
            if (task != null)
            {
                taskDataService.Delete(task);
            }
            Tasks.Remove(task);
            TasksInProcess.Remove(task);

        }

        private void AddTask(object obj)
        {
            var listView = obj as ListView;
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
