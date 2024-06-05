
using ITopiaMAUI.Models;
using System.ComponentModel;

namespace ITopiaMAUI.ViewModels
{
    public class NovellaScenarioViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        NovellaScenarioListViewModel nslvm;
        public DBNovellaScenario NovellaScenario { get; set; }
        public NovellaScenarioViewModel()
        {
            NovellaScenario = new DBNovellaScenario();
        }
        public NovellaScenarioListViewModel NovellaScenarioListViewModel
        {
            get { return nslvm; }
            set
            {
                if (nslvm == value) return;
                nslvm = value;
                OnPropertyChanged("NovellaScenarioListViewModel");
            }
        }
        public string Name
        {
            get { return NovellaScenario.Name; }
            set
            {
                if (NovellaScenario.Name == value) return;
                NovellaScenario.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Scenario
        {
            get { return NovellaScenario.Scenario; }
            set
            {
                if (NovellaScenario.Scenario == value) return;
                NovellaScenario.Scenario = value;
                OnPropertyChanged("Scenario");
            }
        }
        public string Author
        {
            get { return NovellaScenario.Author; }
            set
            {
                if (NovellaScenario.Author == value) return;
                NovellaScenario.Author = value;
                OnPropertyChanged("Author");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Scenario, Author }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
