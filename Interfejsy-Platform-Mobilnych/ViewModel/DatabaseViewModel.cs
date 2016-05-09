using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    [DataContract]
    internal class DatabaseViewModel
    {
        private const string PatternUrl = "http://www.nbp.pl/kursy/xml/dir";
        private const string PatternFileExtension = ".txt";
        private const string PatternUrl1 = "http://www.nbp.pl/kursy/xml/";
        private const string PatternFileExtension1 = ".xml";
        private const int MinAvailableYear = 2002;
        private const string NameDatabaseFile = "Data";

        public readonly ObservableCollection<bool> UiEnabled = new ObservableCollection<bool> {false};

        [DataMember] private DateTimeOffset Date1;

        [DataMember] private DateTimeOffset Date2;

        public string LastState;

        public string LastStateDate;
        public string LastStateHistory;
        public DateTimeOffset MaxDate = new DateTimeOffset(DateTime.Today);

        public DateTimeOffset MinDate = new DateTimeOffset(new DateTime(2002, 1, 2));


        public DatabaseViewModel()
        {
            InitData();
        }

        private ObservableCollection<Year> Database { get; } = new ObservableCollection<Year>();

        private ObservableCollection<Progress> Progress { get; } = new ObservableCollection<Progress>();

        public ObservableCollection<Position> Values { get; } = new ObservableCollection<Position>();

        [DataMember]
        public string SelectedDays { get; } = "LastA";

        [DataMember]
        public string SelectedCurrency { get; set; }

        public void CheckBlackout(CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item != null && HasDate(args.Item.Date) == false)
            {
                args.Item.IsBlackout = true;
            }
        }

        private async void InitData()
        {
            if (Storage.IsFile(NameDatabaseFile))
            {
                foreach (var year in SerializerJson.Deserialize<List<Year>>(Storage.ReadFile(NameDatabaseFile)))
                {
                    Database.Add(year);
                }
                UiEnabled[0] = true;

                if (Connection.IsInternet())
                {
                    //TODO:pobranie aktualizacji
                }
            }
            else
            {
                if (Connection.IsInternet())
                {
                    await DownloadFirstTimeDatabase();

                    UiEnabled[0] = true;
                    Debug.WriteLine("dziala");
                }
            }
        }

        private async Task DownloadFirstTimeDatabase()
        {
            var tmpYear = MinAvailableYear;
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
            Database.Add(PrepareStructure(tmpYear, await Downloader.DownloadString(PatternUrl + PatternFileExtension)));
            await Storage.SaveFile(NameDatabaseFile, SerializerJson.Serialize(Database.ToList()));
        }

        private static Year PrepareStructure(int inYear, string text)
        {
            var year = new Year(inYear);
            foreach (var i in text.Replace("\r\n", "\n").Split('\n'))
            {
                if (!string.IsNullOrEmpty(i) && (i.First() == 'a'))
                    year.Tables.Add(new Table(i));
            }
            return year;
        }

        internal string GetCode(DateTimeOffset? date)
        {
            if (date == null) return null;
            var tmp = date.Value.ToString("yyMMdd");
            return Database[date.Value.Year - MinAvailableYear].Tables.First(x => x.Code.Contains(tmp)).Code;
        }

        internal async void Generate(DateTimeOffset? date1, DateTimeOffset? date2)
        {
            Date1 = date1.Value;
            Date2 = date2.Value;
            for (var i = Values.Count - 1; i > 0; i--)
            {
                Values.RemoveAt(i);
            }
            if (date2 == null) return;
            if (date1 == null) return;
            var tmpYearDif = date2.Value.Year - date1.Value.Year;
            var numberOfDate1 = date1.Value.ToString("yyMMdd");
            var numberOfDate2 = date2.Value.ToString("yyMMdd");
            var tmp = date2.Value.Year - 2002;

            Progress.Add(new Progress(
                Database[tmp].Tables.Find(x => x.GetDate() == numberOfDate1).GetNumber(),
                Database[tmp].Tables.Find(x => x.GetDate() == numberOfDate2).GetNumber()
                ));

            if (tmpYearDif == 0)
            {
                foreach (var pro in Progress)
                {
                    for (pro.Value = pro.Minimum - 1;
                        pro.Value < pro.Maximum - 1;
                        pro.Value++)
                    {
                        var code = Database[tmp].Tables[pro.Value].Code;
                        var output1 = await Downloader.DownloadXml(PatternUrl1 + code + PatternFileExtension1);

                        DateTime date;
                        date = code.Equals("LastA")
                            ? DateTime.Today
                            : new DateTime(int.Parse("20" + code.Substring(5, 2)), int.Parse(code.Substring(7, 2)),
                                int.Parse(code.Substring(9, 2)));

                        var first =
                            DeserializerXml.Deserialize(date, output1)
                                .First(position => position.Name.Equals(SelectedCurrency));
                        Values.Add(first);
                        await Storage.SaveFile(code, output1);
                        //load data
                        //Progress.Clear();
                    }
                }
                //MinValue =(double.Parse(Values.Min().ToString()));
                //MaxValue = (double.Parse(Values.Max().ToString()));
            }
            else if (tmpYearDif == 1)
            {
                //od daty lata[0] do daty [0]last, od lata[1].first do daty [1], 
            }
        }

        private bool HasDate(DateTimeOffset date)
        {
            var table = Database.FirstOrDefault(z => z.Number == int.Parse(date.ToString("yyyy")));
            var enumerable = table.Tables.Select(y => y.GetDate().Equals(date.ToString("yyMMdd")));
            return enumerable.Any(x => x.Equals(true));
        }

        public void SetLatsPage(string state)
        {
            LastState = state;
        }
        
        public async Task SaveStateAsync()
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("store", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, SerializerJson.Serialize(this));
        }

    }
}