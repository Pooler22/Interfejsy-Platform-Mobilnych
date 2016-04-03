using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class Storage
    {
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        StorageFile file;
        
        public async Task createFile(string name)
        {
            file = await roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
        }

        public bool IsFile(string name)
        {
            return System.IO.File.Exists(string.Format(@"{0}\{1}", ApplicationData.Current.RoamingFolder.Path, name));
        }

        public async void saveFile(string nameFile, string data)
        {
            StorageFile file = await roamingFolder.CreateFileAsync(nameFile, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, data);
        }

        public async void readFile(string name)
        {
            await roamingFolder.GetFileAsync(name);
        }

        public string readStringFromFile()
        {
            string text = null;
                Task.Run(
                async () =>
                {
                    text = await FileIO.ReadTextAsync(file);
                })
                .Wait();
            return text;
        }
    }
}
