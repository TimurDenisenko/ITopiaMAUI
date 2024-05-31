
using ITopiaMAUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ITopiaMAUI.ViewModels
{
    public class SaveListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SaveViewModel> Saves { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateSaveCommand { get; protected set; }
        public ICommand DeleteSaveCommand { get; protected set; }
        public ICommand SaveSaveCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public INavigation Navigation { get; set; }
        public SaveListViewModel()
        {
            Saves = new ObservableCollection<SaveViewModel>();
            DeleteSaveCommand = new Command(DeleteSave);
            SaveSaveCommand = new Command(SaveSave);
            BackCommand = new Command(Back);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void Back() => Navigation.PopAsync();
        private void SaveSave(object SaveObj)
        {
            if (SaveObj is not SaveViewModel Save || Save == null || !Save.IsValid || Saves.Contains(Save)) return;
            Saves.Add(Save);
            Back();
        }
        private void DeleteSave(object SaveObj)
        {
            if (SaveObj is not SaveViewModel Save || Save == null) return;
            Saves.Remove(Save);
            Back();
        }
    }
}
