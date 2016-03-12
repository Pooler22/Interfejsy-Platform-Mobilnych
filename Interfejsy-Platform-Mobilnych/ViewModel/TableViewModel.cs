using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        internal void init(Table table)
        {
            if(table != null)
            {
                download(table);
            }
            
        }

        private async void download(Table table)
        {
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            defaultTable.Add(prepareStructure(await Downloader.Get(patternURL + table.table + patternFileExtension)));
        }

        private Table prepareStructure(string v)
        {
            ///xml to object;
            return new Table("asd");
        }
    }
}
