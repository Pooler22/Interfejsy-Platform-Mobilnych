using Interfejsy_Platform_Mobilnych.Models;
using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class DayViewModel
    {
        internal ObservableCollection<Table> tables = new ObservableCollection<Table>();
        public ObservableCollection<Table> Tables { get { return tables; }}
        
        internal async void Init(List<string> codes)
        {
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";

            foreach (string code in getLastRates())
            {
                string output = await Downloader.GetString(patternURL + code + patternFileExtension);
                tables.Add(DeserializerXML.deserialize(code, output));       
            }
        }

        private static string[] getLastRates()
        {
            return new string[] { "LastA", "LastB", "LastC", "LastH" };
        }
    }
}
