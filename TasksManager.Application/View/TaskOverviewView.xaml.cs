using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasksManager.Application.Models;
using TasksManager.Model;

namespace TasksManager.Application.View
{
    /// <summary>
    /// Interaction logic for TaskOverviewView.xaml
    /// </summary>
    public partial class TaskOverviewView : Window
    {
        private TaskModel selectedTask;
        private IEnumerable<TaskModel> tasks;
        TaskDataService taskDataService = new TaskDataService();

        public TaskOverviewView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            TaskListView.ItemsSource = taskDataService.GetAll();
            TaskInProgressListView.ItemsSource = taskDataService.GetAllInProgress();
        }

        private void TaskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTask = e.AddedItems[0] as TaskModel;
            if (e != null)
            {
                this.DataContext = selectedTask;
            }
        }

        private void DetailTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailView taskDetailView = new TaskDetailView();
            taskDetailView.SelectedTask = selectedTask;
            taskDetailView.ShowDialog();
        }
    }
}
