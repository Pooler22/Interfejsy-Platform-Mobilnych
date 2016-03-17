using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        private static DatabaseViewModel instance;
        public static DatabaseViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseViewModel();
                }
                return instance;
            }
        }

        private DatabaseViewModel() { }

        public async Task Init()
        {
            //zczytywanie z pliku
            Storage storage = new Storage();
            string nameFile = "database";
            if (storage.IsFolder(nameFile))
            {
                if (Connection.IsInternet())
                {
                    await storage.createFile(nameFile);
                    storage.readFile(nameFile);
                    foreach(Year year in SerializerJSON.Serializer.deserialize<List<Year>>(storage.readStringFromFile()))
                    {
                        defaultDatabase.Add(year);
                    }
                    
                    //pobranie aktualizacji
                }
                else
                {
                    await storage.createFile(nameFile);
                    storage.readFile(nameFile);
                    foreach (Year year in SerializerJSON.Serializer.deserialize<List<Year>>(storage.readStringFromFile()))
                    {
                        defaultDatabase.Add(year);
                    }
                    //wyświetlnie ostatnich
                }
            }
            else
            {
                if (Connection.IsInternet())
                {
                    DownloadFirstTimeDatabase();
                    foreach(Year year in defaultDatabase)
                    {
                        storage.saveFile(nameFile, SerializerJSON.Serializer.serialize(year));
                    }

                    

                    //zapis danych
                }
                else
                {
                    //brak jakichkolwiek danych
                }
            }

            //Wczytanie struktury
            //Sprawdzenie czy sa nowsze dane w intrenecie
            //Pobranie nowych zmian
            //Zapis nowych zmian
            //Otworzenie najnowszej pozycji

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

            foreach (var i in text.Replace("\r", "").Split('\n'))
            {
                if (i != "" && i != null)
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
                        year.months[year.months.Count - 1].days[year.months[year.months.Count - 1].days.Count - 1].tables.Add(new Table(i));
                    }
                }
            }
            return year;
        }

        internal Table getTable(string tag)
        {
            //if (tag != null)
            //{
            return defaultDatabase[int.Parse(tag.Substring(5, 2)) - 1].months[int.Parse(tag.Substring(7, 2))].days[int.Parse(tag.Substring(9, 2))].tables[0];
            //}
            //else
            //{
            //    //return last table from database
            //    return new Table("");
            //}
        }
    }
}
