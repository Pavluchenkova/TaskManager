using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interactivity;
using TasksManager.Application.Models;
using TasksManager.Application.Utility;

namespace TasksManager.Application.ViewModel
{
    public class TaskDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TaskModel selectedTask;
        private TaskDataService taskDataService;
        ICommand SaveCommand { get; set; }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public TaskDetailViewModel()
        {
            Messenger.Default.Register<TaskModel>(this, OnTaskReceived);
            taskDataService = new TaskDataService();
            LoadCommands();
        }

        private void LoadCommands()
        {
            SaveCommand = new CustomCommand(SaveTask, CanCaveTask);
        }

        private void SaveTask(object obj)
        {
            taskDataService.Update(selectedTask);
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());            
        }
        private bool CanCaveTask(object obj)
        {
            if (SelectedTask != null)
            {
                return true;
            }
            return false;
        }

        private void OnTaskReceived(TaskModel task)
        {
            SelectedTask = task;
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
    }
}
