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
        private ObservableCollection<Year> database = new ObservableCollection<Year>();
        public ObservableCollection<Year> Database { get { return database; } }

        internal ObservableCollection<Position> positions = new ObservableCollection<Position>();
        public ObservableCollection<Position> Positions { get { return positions; } }

        private string patternURL = "http://www.nbp.pl/kursy/xml/dir";
        private string patternFileExtension = ".txt";
        private int minAvailableYear = 2002;

        public DateTimeOffset MinDate
        {
            get { return new DateTimeOffset(2002, 1, 2, 0, 0, 0, new TimeSpan()); }
        }

        public DateTimeOffset MaxDate
        {
            get { return new DateTimeOffset(DateTime.Today); }
        }

        internal string GetCode(DateTimeOffset? date)
        {
            foreach (Table tab in database[date.Value.Year - 2002].tables)
            {
                string tmp = (date.Value.ToString("yyMMdd"));
                if (tab.code.Contains(tmp))
                {
                    return tab.code;
                }
            }
            return null;
        }

        internal async void Generate(DateTimeOffset? date1, DateTimeOffset? date2)
        {
            Storage storage = new Storage();
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            string output;

            int tmpYearDif = date2.Value.Year - date1.Value.Year;
            string numberOfDate1 = date1.Value.ToString("yyMMdd");
            string numberOfDate2 = date2.Value.ToString("yyMMdd");
            bool isDownload = false;
            if (tmpYearDif == 0)
            {
                foreach (Table tab in database[date2.Value.Year - 2002].tables)
                {
                    if (tab.code.Contains(numberOfDate1))
                    {
                        isDownload = !isDownload;
                        numberOfDate1 = numberOfDate2;
                    }
                    if (isDownload)
                    {
                        await storage.createFile(tab.code);
                        output = await Downloader.GetString(patternURL + tab.code + patternFileExtension);
                        storage.saveFile(tab.code, output);
                        //downloadfile
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
            set { selectedCurrency = value; }
        }

        public DatabaseViewModel()
        {
            Storage storage = new Storage();
            string nameFile = "Data";

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

        internal async void InitPositions(string code)
        {
            positions.Clear();
            Storage storage = new Storage();

            if (storage.IsFile(code) && code != null)
            {
                await storage.createFile(code);
                storage.readFile(code);
                foreach (Position pos in DeserializerXML.deserialize(storage.readStringFromFile()))
                {
                    positions.Add(pos);
                }
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string patternURL = "http://www.nbp.pl/kursy/xml/";
                    string patternFileExtension = ".xml";

                    string output = await Downloader.GetString(patternURL + code + patternFileExtension);
                    foreach (Position pos in DeserializerXML.deserialize(output))
                    {
                        positions.Add(pos);
                    }
                    await storage.createFile(code);
                    storage.saveFile(code, output);
                }
                else
                {
                }
            }
        }

        private async void DownloadFirstTimeDatabase()
        {
            int tmpYear = minAvailableYear;
            string tmpResp;
            while (true)
            {
                tmpResp = await Downloader.Get1(patternURL + tmpYear + patternFileExtension);
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
            database.Add((prepareStructure(tmpYear, await Downloader.Get1(patternURL + patternFileExtension))));
            Storage storage = new Storage();
            string nameFile = "Data";
            storage.saveFile(nameFile, SerializerJSON.Serializer.serialize(database.ToList()));
        }

        private Year prepareStructure(int inYear, string text)
        {
            List<Table> tab = new List<Table>();
            foreach (string i in (text.Remove('\r').Split('\n')))
            {
                if (i != null && i != "" && (i.First() == 'a'))
                    tab.Add(new Table(i));
            }
            return new Year(inYear, tab);
        }
    }
}
