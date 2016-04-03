using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Position
    {
        [DataMember]
        internal List<string> listValues = new List<string>();
        public List<string> ModelListValues { get { return listValues; } }

        public Position(List<string> initValues)
        {
            listValues = initValues;
        }
    }
}
