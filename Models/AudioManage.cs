using Plugin.Maui.Audio;

namespace ITopiaMAUI.Models
{
    public class AudioManage
    {
        public static double Volume { get; set; }
        public IAudioPlayer Player { get; set; }
        public bool IsPausing { get; set; }
        private List<MemoryStream> music = new List<MemoryStream>();
        public AudioManage(byte[] sound)
        {
            Volume = 75;
            Player = new AudioManager().CreatePlayer(new MemoryStream(sound));
            Player.Volume = Volume / 100;
        }
        public AudioManage()
        {
            loadMusic();
            Volume = 75;
        }
        public void Play()
        {
            Player.Volume = Volume / 100;
            Player.Play();
        }
        private void loadMusic()
        {
            for (int i = 1; i < 9; i++)
            {
                music.Add(new MemoryStream(Properties.Resources.ResourceManager.GetObject("music" + i) as byte[]));
            }
        }
        public void PlayMusic()
        {
            if (!Player?.IsPlaying ?? true)
            {
                Player = new AudioManager().CreatePlayer(music[new Random().Next(8)]);
                Player.PlaybackEnded+=async(s, e) => await Task.Run(() => NovellaScenario.MusicPlayer.PlayMusic());
                Play();
            }
        }
        public void ReloadAsync()
        {
            Player.Pause();
            Play();
        }
        public void Pause()
        {
            Player.Pause();
        }
    }
}