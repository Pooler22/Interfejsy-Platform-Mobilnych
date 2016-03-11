using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Day
    {
        public int day { get; set; }
        public List<Table> tables { get; set; }
        
        public Day(int day, string table)
        {
            this.day = day;
            this.tables = new List<Table>();
            this.tables.Add(new Table(table));
        }
    }
}
