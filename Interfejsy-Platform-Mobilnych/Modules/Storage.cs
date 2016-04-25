using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal class Storage
    {
        private readonly StorageFolder _roamingFolder = ApplicationData.Current.RoamingFolder;
        private StorageFile _file;

        public async Task CreateFile(string name)
        {
            _file = await _roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
        }

        public static bool IsFile(string name)
        {
            return File.Exists(string.Format(@"{0}\{1}", ApplicationData.Current.RoamingFolder.Path, name));
        }

        public async void SaveFile(string nameFile, string data)
        {
            var file = await _roamingFolder.CreateFileAsync(nameFile, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, data);
        }

        public async void ReadFile(string name)
        {
            await _roamingFolder.GetFileAsync(name);
        }

        public string ReadStringFromFile()
        {
            string text = null;
            Task.Run(
                async () => { text = await FileIO.ReadTextAsync(_file); })
                .Wait();
            return text;
        }
    }
}