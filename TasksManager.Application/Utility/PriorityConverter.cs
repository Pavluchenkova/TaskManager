using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TasksManager.Model.Entities;

namespace TasksManager.Application.Utility
{
    class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case TaskPriority.High:
                    return "#E04343";
                case TaskPriority.Medium:
                    return "#009688";
                case TaskPriority.Low:
                    return "#7bc6bf"; 
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
