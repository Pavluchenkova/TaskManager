using System;
using System.ComponentModel;
using System.Windows.Input;
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
        private Category _newCategory;
        public ICommand SaveNewCategoryCommand { get; set; }
        private ICommand CancelCommand { get; set; }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Category NewCategory
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
            _newCategory = new Category();
            NewCategory.Id = Guid.NewGuid();

            SaveNewCategoryCommand = new RelayCommand(SaveCategory, CanSaveCategory);
        }
        
        private bool CanSaveCategory(object obj)
        {
            return true;
        }
        
        private void SaveCategory(object obj)
        {            
            Category category = obj as Category;            
            _categoryService.Add(category);
            Messenger.Default.Send<Category>(category);

            _newCategory = new Category();
            NewCategory.Id = Guid.NewGuid();
        }
    }
}
