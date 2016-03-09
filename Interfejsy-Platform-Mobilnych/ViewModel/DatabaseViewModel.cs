using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class DatabaseViewModel
    {
        private ObservableCollection<Year> defaultDatabase = new ObservableCollection<Year>();
        public ObservableCollection<Year> DefaultDatabase { get { return defaultDatabase; } }

        string patternURL = "http://www.nbp.pl/kursy/xml/dir";
        string patternFileExtension = ".txt";
        int minAvailableYear = 2002;
        int maxAvailableYear;

        public DatabaseViewModel()
        {
            downloadYears();
        }

        void downloadYears()
        {

            int tmpYear = minAvailableYear;
            string tmpResp;
            while (true)
            {
                tmpResp = Downloader.Get(patternURL + tmpYear + patternFileExtension);
                if (tmpResp == "")
                {
                    maxAvailableYear = tmpYear;
                    break;
                }
                //defaultDatabase.Add(new Year(tmpYear, tmpResp));
                tmpYear++;
            }

            defaultDatabase.Add(new Year(tmpYear, Downloader.Get(patternURL + patternFileExtension)));
        }

        void updateYears()
        {

        }
    }
}
