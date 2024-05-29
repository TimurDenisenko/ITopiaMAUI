
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
    }
}
