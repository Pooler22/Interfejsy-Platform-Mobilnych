using System.Collections.Generic;
using System.Globalization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Month
    {
        internal int monthName;
        public string ModelMonthName { get { return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(monthName); } }

        internal List<Day> days = new List<Day>();
        public List<Day> ModelDays { get { return days; } }
        
        public override bool Equals(object obj)
        {
            return obj != null && (obj as Month).monthName == monthName;
        }

        public override int GetHashCode()
        {
            return monthName.GetHashCode();
        }
    }
}
