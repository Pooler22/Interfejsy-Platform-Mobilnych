using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Month
    {
        [DataMember]
        internal int number;
        public string ModelMonthName { get { return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(number); } }

        [DataMember]
        internal List<Day> days = new List<Day>();
        public List<Day> ModelDays { get { return days; } }
        
        public Month(int number)
        {
            this.number = number;
        }

        public override bool Equals(object obj)
        {
            return obj != null && (obj as Month).number == number;
        }

        public override int GetHashCode()
        {
            return number.GetHashCode();
        }
    }
}
