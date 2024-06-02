
using SQLite;

namespace ITopiaMAUI
{
    [Table("SaveTable")]
    public class DBSaveModel
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
}
