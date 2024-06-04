using Plugin.Maui.Audio;

namespace ITopiaMAUI.Models
{
    public class AudioManage
    {
        public static double Volume { get; set; }
        public IAudioPlayer Player { get; set; }
        private List<MemoryStream> music = new List<MemoryStream>();
        public AudioManage(byte[] sound)
        {
            Volume = 75;
            Player = new AudioManager().CreatePlayer(new MemoryStream(sound));
            Player.Volume = Volume / 100;
        }
        public AudioManage()
        {
            Volume = 75;
        }
        public void Play()
        {
            Player.Volume = Volume / 100;
            Player.Pause();
        }
        private void loadMusic()
        {
            for (int i = 1; i < 9; i++)
            {
                music.Add(new MemoryStream(Properties.Resources.ResourceManager.GetObject("music" + i) as byte[]));
            }
        }
        public Task PlayMusic()
        {
            loadMusic();
            while (true)
            {
                foreach (MemoryStream item in music)
                {
                    if (!Player?.IsPlaying ?? true)
                    {
                        Player = new AudioManager().CreatePlayer(item);
                        Player.Volume = Volume / 100;
                        Play();
                    }
                }
            }
        }
        public void Reload()
        {
            Pause();
            Play();
        }
        public void Pause() => Player.Pause();
    }
}
