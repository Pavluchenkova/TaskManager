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
        Window CategoryView = null;

        public void ShowDialog()
        {
            CategoryView = new CategoryView();
            CategoryView.ShowDialog();
        }
        public void CloseDialog()
        {
            if (CategoryView != null)
            {
                CategoryView.Close();
            }
        }
    }
}
