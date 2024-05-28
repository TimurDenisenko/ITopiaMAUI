
namespace ITopiaMAUI
{
    public class SaveClass
    {
        public int PageNum { get; set; }
        public string Name { get; set; }
        public string Scenario { get; set; }
        public SaveClass(int pageNum, string name, string scenario)
        {
            PageNum = pageNum;
            Name = name;
            Scenario = scenario;
        }
    }
}
