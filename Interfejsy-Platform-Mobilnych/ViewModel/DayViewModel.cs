﻿using Interfejsy_Platform_Mobilnych.Models;
using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class DayViewModel
    {
        internal ObservableCollection<Table> tables = new ObservableCollection<Table>();
        public ObservableCollection<Table> Tables { get { return tables; } }

        internal string name;
        public string Name { get { return name; } }

        internal async void Init(List<string> codes)
        {
            Storage storage = new Storage();

            if (codes == null)
            {
                codes = getLastRates();
            }
            
            foreach (string code in codes)
            {
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

                        foreach (string code1 in getLastRates())
                        {
                            string output = await Downloader.GetString(patternURL + code1 + patternFileExtension);
                            tables.Add(DeserializerXML.deserialize(code1, output));
                            
                        }
                        foreach(Table tab in tables)
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

        private static List<string> getLastRates()
        {
            return new List<string> { "LastA", "LastB", "LastC", "LastH" };
        }
    }
}
