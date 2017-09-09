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

namespace TasksManager.Application.ViewModel
{
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TaskModel selectedTask;
        ICommand DetailCommand;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TaskModel> tasks;
        private ObservableCollection<TaskModel> tasksInProcess;

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
        public TaskOverviewViewModel()
        {
            LoadData();
            LoadCommand();
        }

        private void LoadCommand()
        {
            DetailCommand = new CustomCommand(ShowTaskDetail, CanShowTaskDetail);
        }
        private void ShowTaskDetail(object obj)
        {
            //TODO
        }

        private bool CanShowTaskDetail(object obj)
        {
            if (SelectedTask != null)
                return true;
            return false;
        }

        private void LoadData()
        {
            TaskDataService taskDataService = new TaskDataService();
            Tasks = taskDataService.GetAll().ToObservableCollection();
            TasksInProcess = taskDataService.GetAllInProgress().ToObservableCollection();
        }
    }
}
