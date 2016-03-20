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
        private ObservableCollection<Year> defaultDatabase = new ObservableCollection<Year>();
        public ObservableCollection<Year> DefaultDatabase { get { return defaultDatabase; } }

        private string patternURL = "http://www.nbp.pl/kursy/xml/dir";
        private string patternFileExtension = ".txt";
        private int minAvailableYear = 2002;
        private int maxAvailableYear;

        public DateTimeOffset MinAvailableDate
        {
            get
            {
                string tmp = defaultDatabase[0].months[0].days[0].Code;
                return new DateTime(int.Parse("20" + tmp.Substring(5, 2)), int.Parse(tmp.Substring(7, 2)), int.Parse(tmp.Substring(9, 2)));
            }
        }

        public DateTimeOffset MaxAvailableDate
        {
            get
            {
                string tmp = defaultDatabase.Last().months.Last().days.Last().Code;
                return new DateTime(int.Parse("20" + tmp.Substring(5, 2)), int.Parse(tmp.Substring(7, 2)), int.Parse(tmp.Substring(9, 2)));
            }
        }

        private string selectedDays;
        public string SelectedDays()
        {
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
        }
        internal void setSelectedDay(string v)
        {
            selectedDays = v;
        }


        private string selectedCurrency;
        public string SelectedCurrency { get; }
        internal void setSelectedCurrency(string v)
        {
            selectedCurrency = v;
        }

        public void Init()
        {
            Storage storage = new Storage();
            string nameFile = "Data";

            if (storage.IsFolder(nameFile))
            {
                storage.readFile(nameFile);
                foreach (Year year in SerializerJSON.Serializer.deserialize<List<Year>>(storage.readStringFromFile()))
                {
                    defaultDatabase.Add(year);
                }

                if (Connection.IsInternet())
                {
                    //pobranie aktualizacji
                }

                //wyświetlnie ostatnich
            }
            else
            {
                if (Connection.IsInternet())
                {
                    DownloadFirstTimeDatabase();
                    foreach (Year year in defaultDatabase)
                    {
                        storage.saveFile(nameFile, SerializerJSON.Serializer.serialize(year));
                    }
                }
                else
                {
                    //brak jakichkolwiek danych
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
                    maxAvailableYear = tmpYear;
                    break;
                }
                else
                {
                    defaultDatabase.Add(prepareStructure(tmpYear, tmpResp));
                    tmpYear++;
                }
            }
            defaultDatabase.Add((prepareStructure(tmpYear, await Downloader.Get1(patternURL + patternFileExtension))));
        }

        private Year prepareStructure(int inYear, string text)
        {
            Year year = new Year();
            year.number = inYear;
            string tmpIM = "", tmpID = "";

            foreach (var i in (text.Replace("\r", "").Split('\n')))
            {
                if (i != "" && i != null && (i.Substring(0, 1) == "a"))
                {
                    if (tmpIM != i.Substring(7, 2))
                    {
                        tmpIM = i.Substring(7, 2);
                        year.months.Add(new Month(int.Parse(tmpIM)));

                    }
                    if (tmpID != i.Substring(9, 2))
                    {
                        tmpID = i.Substring(9, 2);
                        year.months[year.months.Count - 1].days.Add(new Day() { number = i });
                    }
                    else
                    {
                        tmpID = i.Substring(9, 2);
                        year.months[year.months.Count - 1].days[year.months[year.months.Count - 1].days.Count - 1].tables = new Table(i);
                    }
                }
            }
            return year;
        }

        internal Table getTable(string tag)
        {
            //if (tag != null)
            //{
            return defaultDatabase[int.Parse(tag.Substring(5, 2)) - 1].months[int.Parse(tag.Substring(7, 2))].days[int.Parse(tag.Substring(9, 2))].tables;
            //}
            //else
            //{
            //    //return last table from database
            //    return new Table("");
            //}
        }
    }
}
