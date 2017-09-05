using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TasksManager.Application.View
{
    /// <summary>
    /// Interaction logic for TaskDetailView.xaml
    /// </summary>
    public partial class TaskDetailView : Window
    {
        public object SelectedTask { get; set; }

                public TaskDetailView()
        {
            InitializeComponent();
            this.Loaded += TaskDetailView_Loaded;
        }
     
        private void TaskDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = SelectedTask;
        }
    }
}
