using System;
using System.Runtime.Serialization;
using static System.String;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
        public Position(DateTime date, string name, int converter, string code, double value)
        {
            Date = date;
            Name = name;
            Converter = converter;
            Code = code;
            Value = value;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Converter { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public double Value { get; set; }

        public string ValueS => Format("{0:0.00}", Value);
    }
}