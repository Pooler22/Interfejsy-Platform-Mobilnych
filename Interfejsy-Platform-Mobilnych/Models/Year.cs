using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Year
    {
        public int year { get; set; }
        public List<Month> months{ get; set; }
        private string[] monthsValues;

        public Year()
        {
            months = new List<Month>();
        }

        internal void addMonth(string v)
        {
            var tmp = new Month() { month = int.Parse(v) };
            if (!months.Contains(tmp))
            {
                months.Add(tmp);
            }
        }
    }
}
