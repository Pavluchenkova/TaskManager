using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Application.ViewModel;

namespace TasksManager.Application
{
    public class ViewModelLocator
    {
        private TaskOverviewViewModel taskOverviewViewModel =
            new TaskOverviewViewModel();
        private TaskDetailViewModel taskDetailViewModel =
            new TaskDetailViewModel();
        public TaskOverviewViewModel TaskOverviewViewModel
        {
            get
            {
                return taskOverviewViewModel;
            }
        }
        public TaskDetailViewModel TaskDetailViewModel
        {
            get
            {
                return taskDetailViewModel;
            }
        }
    }
}
