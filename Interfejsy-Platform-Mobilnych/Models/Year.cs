﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Year
    {
        [DataMember]
        internal int number;
        public int ModelNumber { get { return number; } }

        [DataMember]
        internal List<Table> tables = new List<Table>();
        public List<Table> ModelTables { get { return tables; } }
    }
}
