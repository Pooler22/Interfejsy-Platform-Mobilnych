using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Day
    {
        internal string name;
        public string ModelName { get { return name; }}

        internal List<Table> tables = new List<Table>();
        public List<Table> ModelTables { get { return tables; } }
    }
}
