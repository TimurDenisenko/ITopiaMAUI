
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
            get { return slvm; }
            set
            {
                if (slvm == value) return;
                slvm = value;
                OnPropertyChanged("SaveListViewModel");
            }
        }
        public int PageNum
        {
            get { return Save.PageNum; }
            set
            {
                if (Save.PageNum == value) return;
                Save.PageNum = value;
                OnPropertyChanged("PageNum");
            }
        }
        public string Name
        {
            get { return Save.Name; }
            set
            {
                if (Save.Name == value) return;
                Save.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Scenario
        {
            get { return Save.Scenario; }
            set
            {
                if (Save.Scenario == value) return;
                Save.Scenario = value;
                OnPropertyChanged("Scenario");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Name, PageNum == 0 ? string.Empty : "fill", Scenario }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
