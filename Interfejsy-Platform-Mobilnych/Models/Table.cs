using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Table
    {
        [DataMember]
        internal List<Position> positions = new List<Position>();
        public List<Position> Positions { get { return positions; } }

        [DataMember]
        internal List<string> listKeys = new List<string>();
        public List<string> ModelListKeys { get { return listKeys; } }

        [DataMember]
        internal string code;
        [DataMember]
        internal string name;

        public string ModelName { get { return name; } }

        public Table(string code)
        {
            this.code = code;
            name = ToString();
        }

        public override string ToString()
        {
            char test;
            if (code.Contains("Last"))
            {
                test = (code.ToLower())[code.Length - 1];
            }
            else
            {
                test = code[0];
            }
            switch (test)
            {
                case 'a':
                    return "kursy średnich walut obcych";
                default:
                    return "no name";
            }
        }
    }
}
