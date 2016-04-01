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

        internal Table tables;
        public Table ModelTables { get { return tables; } }
    }
}
