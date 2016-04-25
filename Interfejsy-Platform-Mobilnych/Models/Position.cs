using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
        [DataMember] private readonly List<string> _listValues = new List<string>();
        public string name;
        public string fullname;
        public string value;

        public Position(List<string> initValues)
        {
            _listValues = initValues;
            name = initValues[0];
            fullname = initValues[1];
            value = initValues[2];
        }

        public List<string> ListValues => _listValues;
    }
}