
using SQLite;

namespace ITopiaMAUI.Models
{
    [Table("NovellaScenario")]
    public class DBNovellaScenario
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Scenario { get; set; }
        public string Author { get; set; }
    }
}
