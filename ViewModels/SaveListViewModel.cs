
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
        SaveViewModel selectedSave;
        public INavigation Navigation { get; set; }
        public SaveListViewModel()
        {
            Saves = new ObservableCollection<SaveViewModel>();
            CreateSaveCommand = new Command(CreateSave);
            DeleteSaveCommand = new Command(DeleteSave);
            SaveSaveCommand = new Command(SaveSave);
            BackCommand = new Command(Back);
        }
        public SaveViewModel SelectedSave
        {
            get { return selectedSave; }
            set
            {
                if (selectedSave == value) return;
                SaveViewModel temp = value;
                selectedSave = null;
                OnPropertyChanged("SelectedSave");
                Navigation.PushAsync(new GameView(temp));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreateSave() => Navigation.PushAsync(new GameView(new SaveViewModel() { SaveListViewModel = this }));
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
