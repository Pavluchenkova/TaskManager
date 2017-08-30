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
using TasksManager.Model;

namespace TasksManager.Application.View
{
    /// <summary>
    /// Interaction logic for TaskOverviewView.xaml
    /// </summary>
    public partial class TaskOverviewView : Page
    {
        private Task selectedTask;
        private IEnumerable<Task> tasks;
        public TaskOverviewView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            TaskDataService taskDataService = new TaskDataService();
            tasks= taskDataService.GetAll();
            TaskListView.ItemsSource = tasks;
        }

        private void TaskListView_SelectionChanged( object sender, SelectionChangedEventArgs e)
        {
            selectedTask = e.AddedItems[0] as Task;
            if (e!=null)
            {
            this.DataContext = selectedTask;
            }
            
        }
    }
}
