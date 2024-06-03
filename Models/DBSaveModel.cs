
using SQLite;

namespace ITopiaMAUI
{
    [Table("SaveTable")]
    public partial class DBSaveModel
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int PageNum { get; set; }
        public string Name { get; set; }
        public string Scenario { get; set; }
        public string CurrentBackground { get; set; }
        public string CurrentPers {  get; set; }
        public string RevBackground { get; set; }
        public string RevPers { get; set; }
    }
    public partial class DBSaveModel
    {
        public DBSaveModel() { }
        public DBSaveModel(DBSaveModel save)
        {
            ID = save.ID;
            PageNum = save.PageNum;
            Name = save.Name;
            Scenario = save.Scenario;
            CurrentBackground = save.CurrentBackground;
            CurrentPers = save.CurrentPers;
            RevBackground = save.RevBackground;
            RevPers = save.RevPers;
        }
    }
}
