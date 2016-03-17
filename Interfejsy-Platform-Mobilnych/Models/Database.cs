using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Database
    {
        [DataMember]
        internal List<Year> database = new List<Year>();
        public List<Year> ModelDatabase { get { return database; } }
    }
}
