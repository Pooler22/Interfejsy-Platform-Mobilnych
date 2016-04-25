using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    internal class DatabaseViewModel
    {
        private static readonly string PatternUrl = "http://www.nbp.pl/kursy/xml/dir";
        private static readonly string PatternFileExtension = ".txt";
        private static readonly int MinAvailableYear = 2002;

        private ObservableCollection<Year> Database { get; } = new ObservableCollection<Year>();

        public DateTimeOffset MinDate = new DateTimeOffset(new DateTime(2002, 1, 2));
        public DateTimeOffset MaxDate = new DateTimeOffset(DateTime.Today);

        internal string GetCode(DateTimeOffset? date)
        {
            if (date == null) return null;
            var tmp = (date.Value.ToString("yyMMdd"));
            return Database[date.Value.Year - MinAvailableYear].tables.First(x => x.code.Contains(tmp)).code;
        }

        public ObservableCollection<Progress> Progress { get; } = new ObservableCollection<Progress>();

        internal async void Generate(DateTimeOffset? date1, DateTimeOffset? date2)
        {
            var storage = new Storage();
            if (date2 == null) return;
            if (date1 == null) return;
            var tmpYearDif = date2.Value.Year - date1.Value.Year;
            var numberOfDate1 = date1.Value.ToString("yyMMdd");
            var numberOfDate2 = date2.Value.ToString("yyMMdd");
            var tmp = date2.Value.Year - 2002;

            Progress.Add(new Progress(
                Database[tmp].tables.Find(x => x.GetDate() == numberOfDate1).GetNumber(),
                Database[tmp].tables.Find(x => x.GetDate() == numberOfDate2).GetNumber()
                ));

            if (tmpYearDif == 0)
            {
                

                //progress[0].downloadedMinimum = database[tmp].tables.Find(x => x.getDate() == numberOfDate1).getNumber();
                //progress[0].downloadedMaximum = database[tmp].tables.Find(x => x.getDate() == numberOfDate2).getNumber();
                //progress[0].downloadedValue = progress[0].downloadedMinimum;
                foreach(Progress pro in Progress)
                {
                    for (pro.DownloadedValue = pro.DownloadedMinimum - 1; pro.DownloadedValue < pro.DownloadedMaximum - 1; pro.DownloadedValue++)
                    {
                        string code = Database[tmp].tables[pro.DownloadedValue].code;
                        //download
                        string patternUrl1 = "http://www.nbp.pl/kursy/xml/";
                        string patternFileExtension1 = ".xml";
                        string output1 = await Downloader.DownloadXml(patternUrl1 + code + patternFileExtension1);

                        //save
                        await storage.CreateFile(code);
                        storage.SaveFile(code, output1);
                        //load data
                        Progress.Clear();
                    }
                }
                
            }
            else if (tmpYearDif == 1)
            {
                //od daty lata[0] do daty [0]last, od lata[1].first do daty [1], 
            }
            else
            {
                //od daty lata[0] do daty [0]last, od lata[0<x<l-1].first do daty [0<x<l-1],  od lata[l-1].first do daty [l-1], 
                for (var i = 0; i <= date2.Value.Year - date1.Value.Year; i++)
                {
                }
            }
        }

        public string SelectedDays { get; } = "LastA";

        public string SelectedCurrency { get; private set; }

        public void SetSelectedCurrency(string value)
        {
            SelectedCurrency = value;
        }

        public DatabaseViewModel()
        {
            string nameFile = "Data";
            
            Storage storage = new Storage();

            if (Storage.IsFile(nameFile))
            {
                storage.CreateFile(nameFile);
                storage.ReadFile(nameFile);
                foreach (Year year in SerializerJson.Deserialize<List<Year>>(storage.ReadStringFromFile()))
                {
                    Database.Add(year);
                }

                if (Connection.IsInternet())
                { /*pobranie aktualizacji*/ }
            }
            else
            {
                if (Connection.IsInternet())
                { DownloadFirstTimeDatabase(); }
            }
        }

        private async void DownloadFirstTimeDatabase()
        {
            int tmpYear = MinAvailableYear;
            while (true)
            {
                var tmpResp = await Downloader.DownloadString(PatternUrl + tmpYear + PatternFileExtension);
                if (tmpResp == null)
                {
                    break;
                }
                Database.Add(PrepareStructure(tmpYear, tmpResp));
                tmpYear++;
            }
            Database.Add((PrepareStructure(tmpYear, await Downloader.DownloadString(PatternUrl + PatternFileExtension))));
            Storage storage = new Storage();
            string nameFile = "Data";
            storage.SaveFile(nameFile, SerializerJson.Serialize(Database.ToList()));
        }

        private Year PrepareStructure(int inYear, string text)
        {
            Year year = new Year(inYear);
            foreach (string i in (text.Replace("\r\n", " ").Split(' ')))
            {
                if (!string.IsNullOrEmpty(i) && (i.First() == 'a'))
                    year.AddTable(new Table(i));
            }
            return year;
        }
    }
}
