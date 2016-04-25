using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Converter { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public double Value { get; set; }

        public Position(string a, int b, string c, double d)
        {
            Name = a;
            Converter = b;
            Code = c;
            Value = d;
        }

    }
}