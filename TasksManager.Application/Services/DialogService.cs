using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TasksManager.Application.View;

namespace TasksManager.Application.Services
{
    public class DialogService
    {
        Window taskDetailView = null;

        public void ShowDialog()
        {
            taskDetailView = new TaskDetailView();
            taskDetailView.ShowDialog();
        }
        public void CloseDialog()
        {
            if (taskDetailView != null)
            {
                taskDetailView.Close();
            }
        }
    }
}
