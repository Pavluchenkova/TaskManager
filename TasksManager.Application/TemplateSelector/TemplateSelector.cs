using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TasksManager.Application.Models;

namespace TasksManager.Application.TemplateSelector
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate TaskDataTemplate { get; set; }
        public DataTemplate EditTaskDataTemplate { get; set; }
        public DataTemplate CreateTaskDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            TaskModel task = (TaskModel)item;

            if (task.IsNew == true)
            {
                return CreateTaskDataTemplate;
            }
            if (task.IsModify == true)
            {
                return EditTaskDataTemplate;
            }
            else
            {
                return TaskDataTemplate;
            }
        }
    }
}
