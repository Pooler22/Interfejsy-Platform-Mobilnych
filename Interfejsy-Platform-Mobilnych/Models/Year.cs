using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Year
    {
        [DataMember] private readonly int _number;
        public int Number => _number;

        [DataMember] private readonly List<Table> _tables = new List<Table>();
        public List<Table> Tables => _tables;

        public Year(int initNumber)
        {
            _number = initNumber;
        }

        public void AddTable(Table newTable)
        {
            _tables.Add(newTable);
        }
    }
}
