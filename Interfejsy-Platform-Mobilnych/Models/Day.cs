using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Day
    {
        [DataMember]
        internal string number;
        public string ModelNumber { get { return number.Substring(number.Length - 2, 2); }}
        public string Code { get { return number; } }


        internal List<Table> tables = new List<Table>();
        public List<Table> ModelTables { get { return tables; } }
    }
}
