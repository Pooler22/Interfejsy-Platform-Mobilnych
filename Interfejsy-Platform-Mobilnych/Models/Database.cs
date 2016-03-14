using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Database
    {
        public List<Year> database  { get; }

        public Database(Year year)
        {
            database.Add(year);
        }
    }
}
