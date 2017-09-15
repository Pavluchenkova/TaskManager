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

namespace TasksManager.Application.ViewModel
{
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        private TaskModel newTask;

        public ICommand DetailCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand SaveNewTaskCommand { get; set; }
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

        public TaskModel NewTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                selectedTask = value;
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
            SaveNewTaskCommand = new CustomCommand(SaveTaskCommand, CanSaveTaskCommand);
        }
        private void SaveTaskCommand(object obj)
        {
            Tasks.Add(NewTask);
            taskDataService.Add(Tasks);
        }

        private bool CanSaveTaskCommand(object obj)
        {
            return true;
        }

        private bool CanDeleteTask(object obj)
        {
            if (SelectedTask != null)
            {
                return true;
            }
            return false;
        }

        private void DeleteTask(object obj)
        {
            selectedTask.IsNew = true;
            //taskDataService.Delete(selectedTask);
            Tasks.Remove(selectedTask);
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
            Messenger.Default.Send<TaskModel>(selectedTask);
            dialogService.ShowDialog();
        }

        private bool CanShowTaskDetail(object obj)
        {
            if (SelectedTask != null)
            {
                return true;
            }
            return false;
        }

        private void LoadData()
        {
            Tasks = taskDataService.GetAll().ToObservableCollection();
            TasksInProcess = taskDataService.GetAllInProgress().ToObservableCollection();
        }
    }
}
