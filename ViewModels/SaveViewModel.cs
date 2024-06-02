
using System.ComponentModel;

namespace ITopiaMAUI.ViewModels
{
    public class SaveViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        SaveListViewModel slvm;
        public DBSaveModel Save { get; set; }
        public SaveViewModel()
        {
            Save = new DBSaveModel();
        }
        public SaveListViewModel SaveListViewModel
        {
            get => slvm; 
            set
            {
                if (slvm == value) return;
                slvm = value;
                OnPropertyChanged("SaveListViewModel");
            }
        }
        public int PageNum
        {
            get => Save.PageNum;
            set
            {
                if (Save.PageNum == value) return;
                Save.PageNum = value;
                OnPropertyChanged("PageNum");
            }
        }
        public string Name
        {
            get => Save.Name; 
            set
            {
                if (Save.Name == value) return;
                Save.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Scenario
        {
            get => Save.Scenario; 
            set
            {
                if (Save.Scenario == value) return;
                Save.Scenario = value;
                OnPropertyChanged("Scenario");
            }
        }
        public string CurrentBackground
        {
            get => Save.CurrentBackground ??= "None";
            set
            {
                if (Save.CurrentBackground == value) return;
                Save.CurrentBackground = value;
                OnPropertyChanged("CurrentBackground");
            }
        }
        public string CurrentPers
        {
            get => Save.CurrentPers ??= "None";
            set
            {
                if (Save.CurrentPers == value) return;
                Save.CurrentPers = value;
                OnPropertyChanged("CurrentPers");
            }
        }
        public bool IsValid => new string[] { Name, PageNum == 0 ? string.Empty : "fill", Scenario }.Any(x => !string.IsNullOrEmpty(x?.Trim()));

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
