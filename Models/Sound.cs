using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ITopiaMAUI
{
    public class Sound
    {
        private WaveOutEvent waveOut { get; set; }
        int i = 1;

        public async void Music()
        {
            while (i != 0)
            {
                foreach (var item in new List<byte[]> { })
                {
                    if (i == 0)
                        break;
                    using (MemoryStream stream = new MemoryStream(item))
                    {
                        using (WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(stream)))
                        {
                            try
                            {
                                using (waveOut = new WaveOutEvent())
                                {
                                    waveOut.Init(waveStream);
                                    waveOut.Volume = 1;
                                    waveOut.Play();

                                    while (waveOut.PlaybackState == PlaybackState.Playing)
                                    {
                                        waveOut.Volume = 1;
                                        await Task.Delay(100);
                                        if (i == 0)
                                            break;
                                    }
                                }

                            }
                            catch (Exception) { return; }
                        }
                    }
                }
            }
        }
        public async void Play(byte[] s)
        {
            using (MemoryStream stream = new MemoryStream(s))
            {
                using (WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(stream)))
                {
                    try
                    {
                        using (waveOut = new WaveOutEvent())
                        {
                            waveOut.Init(waveStream);
                            waveOut.Volume = 1;
                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                waveOut.Volume = 1;
                                await Task.Delay(100);
                            }
                        }
                    }
                    catch (Exception) { return; }
                }
            }
        }
        public void Stop()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
            i = 0;
        }
    }
}
