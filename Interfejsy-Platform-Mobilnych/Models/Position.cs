using System;
using System.Runtime.Serialization;
using static System.String;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Position
    {
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

        public Position(DateTime date ,string a, int b, string c, double d)
        {
            Date = date;
            Name = a;
            Converter = b;
            Code = c;
            Value = d;
        }

        public override string ToString()
        {
            return $"[Value: {Value},Date: {Date}]";
        }
    }
}