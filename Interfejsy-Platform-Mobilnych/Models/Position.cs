using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
        [DataMember]
        private readonly List<string> _listValues = new List<string>();
        public List<string> ListValues => _listValues;

        public Position(List<string> initValues)
        {
            _listValues = initValues;
        }
    }
}
