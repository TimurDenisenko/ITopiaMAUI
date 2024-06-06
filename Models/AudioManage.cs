using Plugin.Maui.Audio;

namespace ITopiaMAUI.Models
{
    public class AudioManage
    {
        public static double Volume { get; set; }
        private IAudioPlayer Player { get; set; }
        private readonly List<MemoryStream> music = new List<MemoryStream>();
        private bool musicEnd;
        public AudioManage()
        {
            LoadMusic();
            Volume = 75;
            musicEnd = true;
        }
        public void Play()
        {
            Player.Volume = Volume / 100;
            Player.Play();
        }
        private void LoadMusic()
        {
            for (int i = 1; i < 9; i++)
            { 
                music.Add(new MemoryStream(Properties.Resources.ResourceManager.GetObject("music" + i) as byte[]));
            }
        }
        public async void PlayMusicAsync()
        {
            try
            {
                while (true)
                {
                    if (musicEnd)
                    {
                        musicEnd = false;
                        Player?.Dispose();
                        Player = new AudioManager().CreatePlayer(music[new Random().Next(8)]);
                        Player.PlaybackEnded += (s, e) => musicEnd = true;
                        Play();
                    }
                }
            }
            catch (Exception)
            {
                NovellaScenario.MusicPlayer = new AudioManage();
                await Task.Run(() => NovellaScenario.MusicPlayer.PlayMusicAsync());
            }
        }
        public void ReloadAsync()
        {
            Player.Pause();
            Play();
        }
    }
}