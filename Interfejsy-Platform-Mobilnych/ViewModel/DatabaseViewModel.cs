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

        public DateTimeOffset MinAvailableDate
        {
            get
            {
                string tmp = database[0].tables[0].ModelCode;
                return new DateTime(int.Parse("20" + tmp.Substring(5, 2)), int.Parse(tmp.Substring(7, 2)), int.Parse(tmp.Substring(9, 2)));
            }
        }

        public DateTimeOffset MaxAvailableDate
        {
            get
            {
                string tmp = database.Last().tables.Last().ModelCode;
                return new DateTime(int.Parse("20" + tmp.Substring(5, 2)), int.Parse(tmp.Substring(7, 2)), int.Parse(tmp.Substring(9, 2)));
            }
        }
        
        private string selectedDays;
        public string SelectedDays
        {
            get
            {
                if (selectedDays == null)
                {
                    return "LastA";
                }
                else
                {
                    return selectedDays;
                }
            }
            set { selectedDays = value; }
        }

        private string selectedCurrency;
        public string SelectedCurrency {
            get { return selectedCurrency; }
            set { selectedCurrency = value; }
        }

        public List<string> CurrencyListItems
        {
            get
            {
                //Todo
                List<string> tmp = new List<string>();
                
                //foreach (List<Position> str in tables[0].positions)
                //{
                //    tmp.Add(str[0]);
                //}
                return null;// database[0].months[0].days[0].tables.listKeys;
            }
        }
        
        public DatabaseViewModel()
        {
            Storage storage = new Storage();
            string nameFile = "Data";

            if (storage.IsFile(nameFile))
            {
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
                positions.Add(SerializerJSON.Serializer.deserialize<Position>(storage.readStringFromFile()));
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string patternURL = "http://www.nbp.pl/kursy/xml/";
                    string patternFileExtension = ".xml";

                    string output = await Downloader.GetString(patternURL + code + patternFileExtension);
                    foreach(Position pos in DeserializerXML.deserialize(code, output))
                    {
                        positions.Add(pos);
                    }
                    //foreach (Position position in positions)
                    //{
                    //    storage.saveFile(position.code, SerializerJSON.Serializer.serialize(position));
                    //}
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
            Year year = new Year();
            year.number = inYear;
            string tmpID = "";

            foreach (var i in (text.Replace("\r", "").Split('\n')))
            {
                if (i != null && i != "" && (i.Substring(0, 1) == "a"))
                {
                    tmpID = i.Substring(9, 2);
                    year.tables.Add(new Table(i));
                }
            }
            return year;
        }

        internal Table getTable(string tag)
        {
            return database[int.Parse(tag.Substring(5, 2)) - 1].tables[0];
        }
    }
}
