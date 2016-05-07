﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Charts;
using Position = Interfejsy_Platform_Mobilnych.Models.Position;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    internal class DatabaseViewModel
    {
        private const string PatternUrl = "http://www.nbp.pl/kursy/xml/dir";
        private const string PatternFileExtension = ".txt";
        const string PatternUrl1 = "http://www.nbp.pl/kursy/xml/";
        const string PatternFileExtension1 = ".xml";
        private const int MinAvailableYear = 2002;
        private const string NameDatabaseFile = "Data";

        public DateTimeOffset MinDate = new DateTimeOffset(new DateTime(2002, 1, 2));
        public DateTimeOffset MaxDate = new DateTimeOffset(DateTime.Today);
        
        private ObservableCollection<Year> Database { get; } = new ObservableCollection<Year>();

        public ObservableCollection<Progress> Progress { get; } = new ObservableCollection<Progress>();

        public IList<Position> Values { get; } = new List<Position>();
        public double MinValue { get; set; } = 0.0;
        public double MaxValue { get; set; } = 1.0;

        public ObservableCollection<bool> UIEnabled = new ObservableCollection<bool>() { false };

        public string SelectedDays { get; } = "LastA";

        public string SelectedCurrency { get; set; }

        public DatabaseViewModel()
        {
            InitData();
        }

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
                UIEnabled[0] = true;

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

                    UIEnabled[0] = true;
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
                    year.AddTable(new Table(i));
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
         
            Values.Clear();
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
                //progress[0].downloadedMinimum = database[tmp].tables.Find(x => x.getDate() == numberOfDate1).getNumber();
                //progress[0].downloadedMaximum = database[tmp].tables.Find(x => x.getDate() == numberOfDate2).getNumber();
                //progress[0].downloadedValue = progress[0].downloadedMinimum;
                foreach (var pro in Progress)
                {
                    for (pro.DownloadedValue = pro.DownloadedMinimum - 1;
                        pro.DownloadedValue < pro.DownloadedMaximum - 1;
                        pro.DownloadedValue++)
                    {
                        var code = Database[tmp].Tables[pro.DownloadedValue].Code;
                        var output1 = await Downloader.DownloadXml(PatternUrl1 + code + PatternFileExtension1);

                        DateTime date;
                        date = code.Equals("LastA")
                            ? DateTime.Today
                            : new DateTime(int.Parse("20" + code.Substring(5, 2)), int.Parse(code.Substring(7, 2)), 
                                int.Parse(code.Substring(9, 2)));

                        var first = DeserializerXml.Deserialize(date,output1).First(position => position.Name.Equals(SelectedCurrency));
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

        public bool HasDate(DateTimeOffset date)
        {
            var table = Database.FirstOrDefault(z => z.Number == int.Parse(date.ToString("yyyy")));
            var enumerable = table.Tables.Select(y => y.GetDate().Equals(date.ToString("yyMMdd")));
            return enumerable.Any(x=> x.Equals(true));
        }
        
    }
}