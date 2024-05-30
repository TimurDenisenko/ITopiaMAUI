using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ITopiaMAUI.ViewModels
{
    public class NovellaScenarioListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NovellaScenarioViewModel> NovellaScenarios { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand CreateNovellaScenarioCommand { get; protected set; }
        public ICommand DeleteNovellaScenarioCommand { get; protected set; }
        public ICommand SaveoNovellaScenarioCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public INavigation Navigation { get; set; }
        public NovellaScenarioListViewModel()
        {
            NovellaScenarios = new ObservableCollection<NovellaScenarioViewModel>();
            //CreateNovellaScenarioCommand = new Command(CreateNovellaScenario);
            DeleteNovellaScenarioCommand = new Command(DeleteNovellaScenario);
            SaveoNovellaScenarioCommand = new Command(SaveoNovellaScenario);
            BackCommand = new Command(Back);
        }
        //public NovellaScenarioViewModel SelectedNovellaScenario
        //{
        //    get { return selectedNovellaScenario; }
        //    set
        //    {
        //        if (selectedNovellaScenario == value) return;
        //        NovellaScenarioViewModel temp = value;
        //        selectedNovellaScenario = null;
        //        OnPropertyChanged("SelectedNovellaScenario");
        //        Navigation.PushAsync(new GameView(temp));
        //    }
        //}
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        //private void CreateNovellaScenario() => Navigation.PushAsync(new GameView(new NovellaScenarioViewModel() { NovellaScenarioListViewModel = this }));
        private void Back() => Navigation.PopAsync();
        private void SaveoNovellaScenario(object NovellaScenarioObj)
        {
            if (NovellaScenarioObj is not NovellaScenarioViewModel NovellaScenario || NovellaScenario == null || !NovellaScenario.IsValid || NovellaScenarios.Contains(NovellaScenario)) return;
            NovellaScenarios.Add(NovellaScenario);
            Back();
        }
        private void DeleteNovellaScenario(object NovellaScenarioObj)
        {
            if (NovellaScenarioObj is not NovellaScenarioViewModel NovellaScenario || NovellaScenario == null) return;
            NovellaScenarios.Remove(NovellaScenario);
            Back();
        }
    }
}
