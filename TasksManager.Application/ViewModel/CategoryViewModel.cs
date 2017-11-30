using System;
using System.ComponentModel;
using System.Windows.Input;
using TasksManager.Application.Models;
using TasksManager.Application.Services;
using TasksManager.Application.Utility;
using TasksManager.Model.Entities;

namespace TasksManager.Application.ViewModel
{
    public class CategoryViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DialogService _dialogService = new DialogService();
        private CategoryService _categoryService = new CategoryService();
        private CategoryModel _newCategory;
        public ICommand SaveNewCategoryCommand { get; set; }
        private ICommand CancelCommand { get; set; }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CategoryModel NewCategory
        {
            get { return _newCategory; }
            set
            {
                _newCategory = value;
                RaisePropertyChanged(nameof(NewCategory));
            }
        }

        public CategoryViewModel()
        {
            _newCategory = new CategoryModel();

            SaveNewCategoryCommand = new RelayCommand(SaveCategory, CanSaveCategory);
        }
        
        private bool CanSaveCategory(object obj)
        {
            return true;
        }
        
        private void SaveCategory(object obj)
        {            
            CategoryModel category = obj as CategoryModel;            
            _categoryService.Add(category);
            Messenger.Default.Send<CategoryModel>(category);

            _newCategory = new CategoryModel();
        }
    }
}
