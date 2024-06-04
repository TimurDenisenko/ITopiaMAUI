
using ITopiaMAUI.Models;
using ITopiaMAUI.ViewModels;

namespace ITopiaMAUI
{
    public static class NovellaScenario
    {
        public static SaveViewModel Save { get; set; }
        public static AudioManage MusicPlayer { get; set; }
        public static string Name { get; set; }
        public static int PageNum { get; set; }
        public static int TestNum { get; set; }
        public static int TextSpeed { get; set; }
        public static string[] Scenario { get; set; }
        public static string CurrentBackground { get; set; }
        public static string CurrentPers { get; set; }
        public static string RevBackground { get; set; }
        public static string RevPers { get; set; }
    }
}
