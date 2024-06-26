﻿using Newtonsoft.Json;
namespace ITopiaMAUI
{
    public static class FileManage
    {
        public static string SerializeToFile<T>(T obj) => JsonConvert.SerializeObject(obj);
        public static T DeserializeFile<T>(string json) => JsonConvert.DeserializeObject<T>(json); 
        public static ImageSource ConvertToImageSource(byte[] data) => ImageSource.FromStream(() => new MemoryStream(data));
        public static ImageSource ConvertToImageSource(string data) => ImageSource.FromStream(() => new MemoryStream(Properties.Resources.ResourceManager.GetObject(data) as byte[]));
    }
}
