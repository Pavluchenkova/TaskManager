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
        //public void Update()
        //{
        //    OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Add));
        //}
}
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TaskModel newTask;
        private TaskModel selectedTask;
        private DialogService dialogService = new DialogService();
        private TaskDataService taskDataService;

        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskModel> _tasksInProcess;
        private ObservableCollection<TaskModel> _tasksDone;
        private ObservableCollection<TaskModel> _tasksToDo;

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
        public CustomCommand ChooseInProcessCommand { get; set; }
        public CustomCommand ChooseDoneCommand { get; set; }
        public CustomCommand ChooseToDoCommand { get; set; }

        public ObservableCollection<TaskModel> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }

        public ObservableCollection<TaskModel> TasksInProcess
        {
            get
            {
                return _tasksInProcess;
            }
            set
            {
                _tasksInProcess = value;
                RaisePropertyChanged("TasksInProcess");
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
            EditTaskCommand = new CustomCommand(EditTask, CanEditTask);
            SaveTaskCommand = new CustomCommand(SaveTask, CanSaveTask);
            MakeDoneCommand = new CustomCommand(MakeDone, CanMakeDone);
            ChooseInProcessCommand = new CustomCommand(ChooseInProcess, CanChooseInProcess);
            ChooseDoneCommand = new CustomCommand(ChooseDone, CanChooseDone);
            ChooseToDoCommand = new CustomCommand(ChooseToDo, CanChooseToDo);
        }

        private void ChooseInProcess(object obj)
        {
            Tasks = TasksInProcess;
        }
        private bool CanChooseInProcess(object obj)
        {
            return true;
        }

        private void ChooseToDo(object obj)
        {
            Tasks = TasksToDo;
        }

        private bool CanChooseToDo(object obj)
        {
            return true;
        }

        private void ChooseDone(object obj)
        {
            Tasks = TasksDone;
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
            TasksInProcess.Remove(task);
            Tasks.Remove(task);
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
                if (task.IsModify == true)
                {
                    task.IsModify = false;
                }
            }
            SelectedTask.IsModify = true;
        }
     
        private bool CanSaveTask(object obj)
        {
            return true;
        }
        private void SaveTask(object obj)
        {
            TaskModel task = obj as TaskModel;

            if (task.IsNew == true)
            {
                taskDataService.Add(Tasks);
                task.IsNew = false;
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
                TasksInProcess.Add(task);
            }
            else
            {
                TasksDone.Add(task);
            }
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
            TasksInProcess = taskDataService.GetAllInProgress().ToObservableCollection();
            TasksDone = taskDataService.GetAllDone().ToObservableCollection();
            TasksToDo = taskDataService.GetAllToDo().ToObservableCollection();
            Tasks = TasksToDo;
        }
    }
}
