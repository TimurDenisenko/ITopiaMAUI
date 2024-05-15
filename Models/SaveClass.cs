
namespace ITopiaMAUI
{
    public class SaveClass
    {
        public int PageNum { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public SaveClass(int pageNum, string name, Gender gender)
        {
            PageNum = pageNum;
            Name = name;
            Gender = gender;
        }
    }
}
