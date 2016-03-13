using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class Storage
    {
            StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
            StorageFile file;
            string output;

            public string Output
            {
                get
                {
                    return output;
                }

                set
                {
                    output = value;
                }
            }

            public async Task newFile(string name)
            {
                file = await roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
            }

            public async void readFile(string name)
            {
                StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
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

            public async void saveInFile(string name, string data)
            {
                StorageFile file = await roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
                await FileIO.WriteTextAsync(file, data);
            }
        }
    
}
