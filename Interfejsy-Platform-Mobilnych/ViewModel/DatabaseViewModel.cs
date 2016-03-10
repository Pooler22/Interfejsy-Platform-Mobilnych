using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        async void downloadYears()
        {
            int tmpYear = minAvailableYear;
            string tmpResp;
            while (true)
            {
                tmpResp = await Downloader.Get(patternURL + tmpYear + patternFileExtension);
                if (tmpResp == null)
                {
                    maxAvailableYear = tmpYear;
                    break;
                }
                defaultDatabase.Add((prepareStructure(tmpYear,tmpResp)));
                tmpYear++;
            }
            defaultDatabase.Add((prepareStructure(tmpYear, await Downloader.Get(patternURL + patternFileExtension))));
        }

        private Year prepareStructure(int inYear,string text)
        {
            Year year = new Year();
            year.year = inYear;
            string tmpIM = "", tmpID = "";
            Month tmpMonth = null;
            List<Day> tmpDay = new List<Day>();

            foreach (var i in text.Replace("\r", "").Split('\n'))
            {
                if (i != "" && i != null)
                {
                    if(tmpIM != i.Substring(7, 2))
                    {
                        tmpIM = i.Substring(7, 2);
                        year.months.Add(new Month() { month = int.Parse(tmpIM)});
                        
                    }
                    if (tmpID != i.Substring(9, 2))
                    {
                        tmpID = i.Substring(9, 2);
                        year.months[year.months.Count-1].days.Add(new Day() { day = int.Parse(tmpID) });
                    }
                }
            }

            return year;
        }
    }
}
