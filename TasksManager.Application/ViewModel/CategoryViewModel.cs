using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TasksManager.Application.Services;
using TasksManager.Application.Utility;
using TasksManager.Model.Entities;

namespace TasksManager.Application.ViewModel
{
    public class CategoryViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DialogService dialogService = new DialogService();
        private CategoryService categoryService = new CategoryService();
        private Category _category;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Category newCategory;

        public Category NewCategory
        {
            get
            {
                return newCategory;
            }
            set
            {
                newCategory = value;
                RaisePropertyChanged("NewCategory");
            }
        }

        public ICommand SaveNewCategoryCommand { get; set; }

        private ICommand CancelCommand { get; set; }

        public CategoryViewModel()
        {
            newCategory = new Category();
            NewCategory.Id = Guid.NewGuid();
            NewCategory.Name = "someName";
            Messenger.Default.Register<Category>(this, OnCategoryReceived);

            SaveNewCategoryCommand = new RelayCommand(SaveCategory, CanSaveCategory);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
        }
        public void OnCategoryReceived(Category category)
        {
            _category = category;
        }
        private bool CanSaveCategory(object obj)
        {
            return true;
        }
        
        private void SaveCategory(object obj)
        {            
            Category category = obj as Category;            
            categoryService.Add(category);
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }
        private bool CanCancel(object obj)
        {
            return true;
        }

        private void Cancel(object obj)
        {
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }
    }
}
