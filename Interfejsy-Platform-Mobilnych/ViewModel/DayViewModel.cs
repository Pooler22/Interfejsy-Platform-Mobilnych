using Interfejsy_Platform_Mobilnych.Models;
using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Modules;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class DayViewModel
    {
        internal ObservableCollection<Table> tables = new ObservableCollection<Table>();
        public ObservableCollection<Table> Tables { get { return tables; } }

        internal string name;
        public string Name { get { return name; } }

        internal async void Init(string code)
        {
            name = code;
            tables.Clear();
            Storage storage = new Storage();


            if (storage.IsFolder(code) && code != null)
            {
                await storage.createFile(code);
                storage.readFile(code);
                tables.Add(SerializerJSON.Serializer.deserialize<Table>(storage.readStringFromFile()));
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string patternURL = "http://www.nbp.pl/kursy/xml/";
                    string patternFileExtension = ".xml";

                    string output = await Downloader.GetString(patternURL + code + patternFileExtension);
                    tables.Add(DeserializerXML.deserialize(code, output));

                    foreach (Table tab in tables)
                    {
                        storage.saveFile(tab.code, SerializerJSON.Serializer.serialize(tab));
                    }
                }
                else
                {

                }
            }
        }
    }
}