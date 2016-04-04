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

        internal ObservableCollection<Position> positions = new ObservableCollection<Position>();
        public ObservableCollection<Position> Positions { get { return positions; } }
        
        public DateTimeOffset MinDate = new DateTimeOffset(new DateTime(2002, 1, 2));
        public DateTimeOffset MaxDate = new DateTimeOffset(DateTime.Today);

        internal string GetCode(DateTimeOffset? date)
        {
            string tmp = (date.Value.ToString("yyMMdd"));
            return database[date.Value.Year - minAvailableYear].tables.First((x) => x.code.Contains(tmp)).code;
        }

        internal void Generate(DateTimeOffset? date1, DateTimeOffset? date2)
        {
            Storage storage = new Storage();
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            string output;
            
            int tmpYearDif = date2.Value.Year - date1.Value.Year;
            string numberOfDate1 = date1.Value.ToString("yyMMdd");
            string numberOfDate2 = date2.Value.ToString("yyMMdd");
            string code1, code2;
            int nrCode1, nrCode2;
            bool isDownload = false;
            int tmp = date2.Value.Year - 2002;

            if (tmpYearDif == 0)
            {
                for(int i = 0; i < tmp; i++)
                {
                    if (database[tmp].tables[i].code.Contains(numberOfDate1))
                    {
                        code1 = database[tmp].tables[i].code;
                        nrCode1 = i;
                    }
                    else if (database[tmp].tables[i].code.Contains(numberOfDate2))
                    {
                        code2 = database[tmp].tables[i].code;
                        nrCode2 = i;
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
        }

        public void setSelectedCurrency(string value)
        {
            selectedCurrency = value;
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

                    string output = await Downloader.DownloadXml(patternURL + code + patternFileExtension);
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
            foreach (string i in (text.Replace("\r\n"," ").Split(' ')))
            {
                if (i != null && i != "" && (i.First() == 'a'))
                    year.AddTable(new Table(i));
            }
            return year;
        }
    }
}
