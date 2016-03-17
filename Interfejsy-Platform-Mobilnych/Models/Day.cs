using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Day
    {
        [DataMember]
        internal string number;
        public string ModelNumber { get { return number; }}

        [DataMember]
        internal List<Table> tables = new List<Table>();
        public List<Table> ModelTables { get { return tables; } }
    }
}
