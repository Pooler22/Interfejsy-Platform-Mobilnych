using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class DatabaseViewModel
    {
        private static string patternURL = "http://www.nbp.pl/kursy/xml/dir";
        private static string patternFileExtension = ".txt";
        private static int minAvailableYear = 2002;

        private ObservableCollection<Year> database = new ObservableCollection<Year>();
        public ObservableCollection<Year> Database { get { return database; } }

        public DateTimeOffset MinDate = new DateTimeOffset(new DateTime(2002, 1, 2));
        public DateTimeOffset MaxDate = new DateTimeOffset(DateTime.Today);

        internal string GetCode(DateTimeOffset? date)
        {
            string tmp = (date.Value.ToString("yyMMdd"));
            return database[date.Value.Year - minAvailableYear].tables.First((x) => x.code.Contains(tmp)).code;
        }

        private ObservableCollection<Progress> progress = new ObservableCollection<Progress>() { new Progress() };
        public ObservableCollection<Progress> Progress { get { return progress; } }

        internal async void Generate(DateTimeOffset? date1, DateTimeOffset? date2)
        {
            Storage storage = new Storage();
            int tmpYearDif = date2.Value.Year - date1.Value.Year;
            string numberOfDate1 = date1.Value.ToString("yyMMdd");
            string numberOfDate2 = date2.Value.ToString("yyMMdd");
            int tmp = date2.Value.Year - 2002;

            if (tmpYearDif == 0)
            {
                //progress.Add(new Progress()
                //{
                //    downloadedMinimum = database[tmp].tables.Find(x => x.getDate() == numberOfDate1).getNumber(),
                //    downloadedMaximum = database[tmp].tables.Find(x => x.getDate() == numberOfDate2).getNumber(),
                //    downloadedValue = 0
                //});

                progress[0].downloadedMinimum = database[tmp].tables.Find(x => x.getDate() == numberOfDate1).getNumber();
                progress[0].downloadedMaximum = database[tmp].tables.Find(x => x.getDate() == numberOfDate2).getNumber();
                progress[0].downloadedValue = progress[0].downloadedMinimum;



                for (progress[0].downloadedValue = progress[0].downloadedMinimum - 1; progress[0].downloadedValue < progress[0].downloadedMaximum - 1; progress[0].downloadedValue++)
                {
                    string code = database[tmp].tables[progress[0].downloadedValue].code;
                    //download
                    string patternURL1 = "http://www.nbp.pl/kursy/xml/";
                    string patternFileExtension1 = ".xml";
                    string output1 = await Downloader.DownloadXml(patternURL1 + code + patternFileExtension1);

                    //save
                    await storage.createFile(code);
                    storage.saveFile(code, output1);
                    //load data
                    progress.Clear();
                }
            }
            else if (tmpYearDif == 1)
            {
                //od daty lata[0] do daty [0]last, od lata[1].first do daty [1], 
            }
            else
            {
                //od daty lata[0] do daty [0]last, od lata[0<x<l-1].first do daty [0<x<l-1],  od lata[l-1].first do daty [l-1], 
                for (int i = 0; i <= date2.Value.Year - date1.Value.Year; i++)
                {
                }
            }
        }

        private string selectedDays = "LastA";
        public string SelectedDays
        {
            get { return selectedDays; }
            set { selectedDays = value; }
        }

        private string selectedCurrency;
        public string SelectedCurrency
        {
            get { return selectedCurrency; }
        }

        public void setSelectedCurrency(string value)
        {
            selectedCurrency = value;
        }

        public DatabaseViewModel()
        {
            string nameFile = "Data";
            
            Storage storage = new Storage();

            if (storage.IsFile(nameFile))
            {
                storage.createFile(nameFile);
                storage.readFile(nameFile);
                foreach (Year year in SerializerJSON.Serializer.deserialize<List<Year>>(storage.readStringFromFile()))
                {
                    database.Add(year);
                }

                if (Connection.IsInternet())
                { /*pobranie aktualizacji*/ }
            }
            else
            {
                if (Connection.IsInternet())
                { DownloadFirstTimeDatabase(); }
                else
                { /*brak jakichkolwiek danych*/ }
            }
        }

        private async void DownloadFirstTimeDatabase()
        {
            int tmpYear = minAvailableYear;
            string tmpResp;
            while (true)
            {
                tmpResp = await Downloader.DownloadString(patternURL + tmpYear + patternFileExtension);
                if (tmpResp == null)
                {
                    break;
                }
                else
                {
                    database.Add(prepareStructure(tmpYear, tmpResp));
                    tmpYear++;
                }
            }
            database.Add((prepareStructure(tmpYear, await Downloader.DownloadString(patternURL + patternFileExtension))));
            Storage storage = new Storage();
            string nameFile = "Data";
            storage.saveFile(nameFile, SerializerJSON.Serializer.serialize(database.ToList()));
        }

        private Year prepareStructure(int inYear, string text)
        {
            Year year = new Year(inYear);
            foreach (string i in (text.Replace("\r\n", " ").Split(' ')))
            {
                if (i != null && i != "" && (i.First() == 'a'))
                    year.AddTable(new Table(i));
            }
            return year;
        }
    }
}
