using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Year
    {
        [DataMember]
        internal int number;
        public int ModelNumber{ get { return number; } }

        [DataMember]
        internal List<Month> months = new List<Month>();
        public List<Month> ModelMonths { get {return months; } }

        internal void addMonth(string name)
        {
            var tmp = new Month(int.Parse(name));
            if (!months.Contains(tmp))
            {
                months.Add(tmp);
            }
        }
    }
}
