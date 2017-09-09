using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Application.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            var observableCollection = new ObservableCollection<T>();
            foreach (var item in coll)
            {
                observableCollection.Add(item);
            }
            return observableCollection;
        }
    }
}
