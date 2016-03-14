using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfejsy_Platform_Mobilnych.Models;
using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Diagnostics;

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


        internal void init(Table table)
        {
            if(table != null && table.Code != null)
            {
                download(table);
            }
        }

        private async void download(Table table)
        {
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            string a = await Downloader.GetString(patternURL + table.Code + patternFileExtension);
            defaultTable.Add(prepareStructure(defaultPositions,table, a));
        }

        private Table prepareStructure(ObservableCollection<Position> defaultPositions, Table table, string a)
        {
            return DeserializerXML.deserialize(defaultPositions, table, a);
        }


    }
}
