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
        private static TaskOverviewViewModel taskOverviewViewModel =
            new TaskOverviewViewModel();
        private static CategoryViewModel categoryViewModel =
            new CategoryViewModel();
        public static TaskOverviewViewModel TaskOverviewViewModel
        {
            get
            {
                return taskOverviewViewModel;
            }
        }
        public static CategoryViewModel CategoryViewModel
        {
            get
            {
                return categoryViewModel;
            }
        }

    }
}
