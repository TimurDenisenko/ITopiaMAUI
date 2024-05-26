
using SQLite.Net.Attributes;

namespace ITopiaMAUI.Models
{
    [Table("NovellaScenario")]
    public class DBNovellaScenario
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Scenario { get; set; }
        public string Author { get; set; }
    }
}
