using System;
using System.ComponentModel;

namespace TasksManager.Application.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        private string _name;

        public CategoryModel()
        {
            CategoryId = Guid.NewGuid();
        }

        public Guid CategoryId { get; set; }

        public string Name
        {
            get { return _name; }

            set
            {
                SetValue(ref _name, value);
            }
        }

        private void SetValue<T>(ref T variable, T value, string propertyName = null)
        {
            variable = value;
            RaisePropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
