using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Database
    {
        internal List<Year> database = new List<Year>();
        public List<Year> ModelDatabase { get { return database; } }
    }
}
