using Interfejsy_Platform_Mobilnych.Models;
using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Modules;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class TableViewModel
    {
        private ObservableCollection<Table> defaultTable = new ObservableCollection<Table>();
        public ObservableCollection<Table> DefaultTable
        {
            get { return defaultTable; }
        }

        private ObservableCollection<Position> defaultPositions = new ObservableCollection<Position>();
        public ObservableCollection<Position> DefaultPositions
        {
            get { return defaultPositions; }
        }

        public void Init(Table table)
        {
            if (table != null && table.code!= null)
            {
                download(table);
            }
        }

        private async void download(Table table)
        {
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            string output = await Downloader.GetString(patternURL + table.code + patternFileExtension);
            defaultTable.Add(prepareStructure(defaultPositions, table, output));
        }

        private Table prepareStructure(ObservableCollection<Position> defaultPositions, Table table, string a)
        {
            return DeserializerXML.deserialize(defaultPositions, table, a);
        }


    }
}
