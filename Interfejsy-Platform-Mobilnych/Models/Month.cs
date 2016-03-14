using System.Collections.Generic;
using System.Globalization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Month
    {
        public int month { get; set; }

        public string MonthName
        {
            get
            {
                return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
            }
        }
        public List<Day> days { get; set; }

        public Month()
        {
            days = new List<Day>();
        }

        public override bool Equals(object obj)
        {
            Month q = obj as Month;
            return q != null && q.month == this.month;
        }

        public override int GetHashCode()
        {
            return month.GetHashCode();
        }
    }
}
