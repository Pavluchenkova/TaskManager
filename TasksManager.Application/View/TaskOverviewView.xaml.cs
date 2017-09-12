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
using TasksManager.Application.ViewModel;
using TasksManager.Model;

namespace TasksManager.Application.View
{
    /// <summary>
    /// Interaction logic for TaskOverviewView.xaml
    /// </summary>
    public partial class TaskOverviewView : Window
    {
        public TaskOverviewView()
        {
            InitializeComponent();
          //  DataContext = new TaskOverviewViewModel();
        }

    }
}
