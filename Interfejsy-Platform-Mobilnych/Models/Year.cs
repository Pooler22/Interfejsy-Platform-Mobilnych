using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Year
    {
        [DataMember] private int number;
        public int Number => number;

        [DataMember]
        internal List<Table> tables = new List<Table>();
        public List<Table> Tables => tables;

        public Year(int initNumber)
        {
            number = initNumber;
        }

        public void AddTable(Table newTable)
        {
            tables.Add(newTable);
        }
    }
}
