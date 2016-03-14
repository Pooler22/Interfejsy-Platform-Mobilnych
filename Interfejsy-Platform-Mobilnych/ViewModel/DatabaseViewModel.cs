﻿using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Collections.ObjectModel;
using System;

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

        public void Init()
        {
            downloadYears();
        }


        private static DatabaseViewModel instance;

        private DatabaseViewModel() { }

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




        async void downloadYears()
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
                defaultDatabase.Add((prepareStructure(tmpYear,tmpResp)));
                tmpYear++;
            }
            defaultDatabase.Add((prepareStructure(tmpYear, await Downloader.Get1(patternURL + patternFileExtension))));
        }

        private Year prepareStructure(int inYear,string text)
        {
            Year year = new Year();
            year.year = inYear;
            string tmpIM = "", tmpID = "";

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
                        year.months[year.months.Count - 1].days.Add( new Day(int.Parse(tmpID), i));
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
            if (tag != null)
            {
                return defaultDatabase[int.Parse(tag.Substring(5, 2)) - 1].months[int.Parse(tag.Substring(7, 2))].days[int.Parse(tag.Substring(9, 2))].tables[0];
            }
            else
            {
                //return last table from database
                return new Table("");
            }
        }
    }
}
