using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TasksManager.Application.Models;
using System.Windows.Input;
using TasksManager.Application.Services;
using TasksManager.Application.Utility;


namespace TasksManager.Application.ViewModel
{
    public class TaskOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TaskModel _newTask;
        private TaskModel _selectedTask;
        private TaskStatusModel _filterName;
        private TaskDataService _taskDataService;
        private CategoryService _categoryService;
        private DialogService _dialogService = new DialogService();
        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskModel> _tasksInProgress;
        private ObservableCollection<TaskModel> _tasksDone;
        private ObservableCollection<TaskModel> _tasksToDo;
        private ObservableCollection<CategoryModel> _categories;
        private CategoryModel _selectedCategory;
        private CategoryModel _newCategory;

        public ICommand DetailCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand SaveTaskCommand { get; set; }
        public ICommand MakeDoneCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                RaisePropertyChanged(nameof(Tasks));
            }
        }

        public ObservableCollection<TaskModel> TasksInProgress
        {
            get { return _tasksInProgress; }
            set
            {
                _tasksInProgress = value;
                RaisePropertyChanged(nameof(TasksInProgress));
            }
        }

        public ObservableCollection<TaskModel> TasksDone
        {
            get { return _tasksDone; }
            set
            {
                _tasksDone = value;
                RaisePropertyChanged(nameof(TasksDone));
            }
        }
        public ObservableCollection<TaskModel> TasksToDo
        {
            get { return _tasksToDo; }
            set
            {
                _tasksToDo = value;
                RaisePropertyChanged(nameof(TasksToDo));
            }
        }
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }
        public TaskModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                RaisePropertyChanged(nameof(SelectedTask));
            }
        }
        public TaskModel NewTask
        {
            get { return _newTask; }
            set
            {
                _newTask = value;
                RaisePropertyChanged(nameof(NewTask));
            }
        }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }

            set
            {
                _selectedCategory = value;

                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }
        
        public CategoryModel NewCategory
        {
            get { return _newCategory; }

            set
            {
                _newCategory = value;
                RaisePropertyChanged(nameof(NewCategory));
            }
        }

        public TaskStatusModel FilterName
        {
            get { return _filterName; }
            set
            {
                switch (value)
                {
                    case TaskStatusModel.ToDo:
                        Tasks = TasksToDo;
                        break;
                    case TaskStatusModel.InProgress:
                        Tasks = TasksInProgress;
                        break;
                    case TaskStatusModel.Done:
                        Tasks = TasksDone;
                        break;
                }
                _filterName = value;
                RaisePropertyChanged(nameof(FilterName));
            }
        }

        public IEnumerable<TaskStatusModel> TaskStatuses
        {
            get
            {
                return Enum.GetValues(typeof(TaskStatusModel)).Cast<TaskStatusModel>();
            }
        }

        public IEnumerable<TaskPriorityModel> TaskPriorities
        {
            get
            {
                return Enum.GetValues(typeof(TaskPriorityModel)).Cast<TaskPriorityModel>();
            }
        }
        public TaskOverviewViewModel()
        {
            _taskDataService = new TaskDataService();
            _categoryService = new CategoryService();

            LoadData();
            LoadCommands();

            Messenger.Default.Register<CategoryModel>(this, OnCategoryReceived);
        }

        private void LoadCommands()
        {
            AddTaskCommand = new RelayCommand(AddTask, CanAddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            EditTaskCommand = new RelayCommand(EditTask, CanEditTask);
            SaveTaskCommand = new RelayCommand(SaveTask, CanSaveTask);
            MakeDoneCommand = new RelayCommand(MakeDone, CanMakeDone);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            AddCategoryCommand = new RelayCommand(AddCategory, CanAddCategory);
        }

        private void OnCategoryReceived(CategoryModel obj)
        {
            NewCategory = obj;
            _dialogService.CloseDialog();
            Categories.Add(NewCategory);
        }
        private bool CanAddCategory(object obj)
        {
            return true;
        }

        private void AddCategory(object obj)
        {
            _dialogService.ShowDialog();
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
                var ind = Tasks.IndexOf(task);
                Tasks.Remove(task);
                task = _taskDataService.GetById(task.TaskId);
                Tasks.Insert(ind,task);
            }
        }

        private bool CanMakeDone(object obj)
        {
            return true;
        }

        private void MakeDone(object obj)
        {
            var task = obj as TaskModel;
            task.Status = TaskStatusModel.Done;
            task.FinishDate = DateTime.Now;
            _taskDataService.Update(task);

            TasksDone.Insert(0,task);
            TasksInProgress.Remove(task);
            Tasks.Remove(task);
        }

        private void AddTask(object obj)
        {
            NewTask = new TaskModel();
            NewTask.IsNew = true;
            Tasks.Insert(0, NewTask);
        }

        private bool CanAddTask(object obj)
        {
            if (NewTask != null)
            {
                return false;
            }

            return true;
        }

        private void EditTask(object obj)
        {
            SelectedTask = obj as TaskModel;          
            SelectedCategory = Categories.FirstOrDefault(c=>c.CategoryId==SelectedTask.CategoryId);

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
                if (SelectedCategory != null)
                {
                    task.CategoryId = SelectedCategory.CategoryId;
                    task.Category = SelectedCategory;
                }
                _taskDataService.Add(task); 
                task.IsNew = false;
                NewTask = null;
            }
            else
            {
                if (SelectedCategory != null)
                {
                    task.Category = SelectedCategory;
                    task.CategoryId = SelectedCategory.CategoryId;
                }
                _taskDataService.Update(task);
                task.IsModify = false;
            }

            var ind = Tasks.IndexOf(task);

            Tasks.RemoveAt(ind);
            if (task.Status == TaskStatusModel.ToDo)
            {
                TasksToDo.Insert(0, task);
            }
            else if (task.Status == TaskStatusModel.InProgress)
            {
                TasksInProgress.Insert(0, task);
            }
            else
            {
                task.FinishDate = DateTime.Now;
                TasksDone.Insert(0, task);
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
                _taskDataService.Delete(task);
            }
            Tasks.Remove(task);
        }

        private bool CanDeleteTask(object obj)
        {
            return true;
        }

        private void LoadData()
        {
            TasksInProgress = new ObservableCollection<TaskModel>(_taskDataService.GetAllInProgress());
            TasksDone = new ObservableCollection<TaskModel>(_taskDataService.GetAllDone());
            TasksToDo = new ObservableCollection<TaskModel>(_taskDataService.GetAllToDo());            
            Tasks = TasksToDo;

            Categories = new ObservableCollection<CategoryModel>(_categoryService.GetAll());
        }
    }
}
