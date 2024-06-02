
using ITopiaMAUI.ViewModels;

namespace ITopiaMAUI
{
    public static class NovellaScenario
    {
        public static string Name { get; set; }
        public static string CurrentLocation { get; set; }
        public static int PageNum { get; set; }
        public static string[] Scenario { get; set; }
        public static bool Change { get; set; }
        public static SaveViewModel Save { get; set; }
        public static string CurrentBackground
        {
            get => CurrentBackground;
            set
            {
                RevBackground = CurrentBackground;
                CurrentBackground = value;
            }
        }
        public static string CurrentPers
        {
            get => CurrentPers;
            set
            {
                RevPers = CurrentPers;
                CurrentPers = value;
            }
        }
        public static string RevBackground { get; set; }
        public static string RevPers { get; set; }
    }
}
