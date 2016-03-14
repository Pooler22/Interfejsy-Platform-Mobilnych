using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Day
    {
        public int day { get; set; }
        public List<Table> tables { get; set; }
        
        public Day(int dayNumber, string table)
        {
            day = dayNumber;
            tables = new List<Table>();
            tables.Add(new Table(table));
        }
    }
}
