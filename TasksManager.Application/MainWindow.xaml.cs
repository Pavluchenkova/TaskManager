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
using TasksManager.Infrastructure.DAL;
using TasksManager.Infrastructure.Repository;
using Task = TasksManager.Model.Task;

namespace TasksManager.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TaskRepository _repository = new TaskRepository();
        public MainWindow()
        {
        //    InitializeComponent();
        //    Task task = _repository.GetBy(e => e.Category == Model.TaskCategory.Study);
        //    string TaskName = task.Title;
        //    testTextBlock.Text = TaskName;
        }       
}
   }
