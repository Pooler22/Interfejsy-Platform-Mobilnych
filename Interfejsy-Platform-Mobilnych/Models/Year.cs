using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Year
    {
        internal int name;
        public int ModelName { get { return name; } }

        internal List<Month> months = new List<Month>();
        public List<Month> ModelMonths { get {return months; } }

        internal void addMonth(string v)
        {
            var tmp = new Month() { monthName = int.Parse(v) };
            if (!months.Contains(tmp))
            {
                months.Add(tmp);
            }
        }
    }
}
