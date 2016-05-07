using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class Storage
    {
        public static bool IsFile(string name)
        {
            return File.Exists(string.Format(@"{0}\{1}", ApplicationData.Current.RoamingFolder.Path, name));
        }

        public static async Task SaveFile(string nameFile, string data)
        {
            var file = await
                ApplicationData.Current.RoamingFolder.CreateFileAsync(nameFile, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, data);
        }

        public static string ReadFile(string name)
        {
            string text = null;
            Task.Run(
                async () =>
                {
                    var file = await ApplicationData.Current.RoamingFolder.GetFileAsync(name);
                    text = await FileIO.ReadTextAsync(file);
                })
                .Wait();
            return text;
        }
    }
}