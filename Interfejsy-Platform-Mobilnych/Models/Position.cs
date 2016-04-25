using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
        [DataMember] private readonly List<string> _listValues = new List<string>();

        public Position(List<string> initValues)
        {
            _listValues = initValues;
        }

        public List<string> ListValues => _listValues;
    }
}