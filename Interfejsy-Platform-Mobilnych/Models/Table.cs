using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Table
    {
        internal List<Position> positions = new List<Position>();
        public List<Position> Positions { get { return positions; } }

        internal List<string> listKeys = new List<string>();
        public List<string> ModelListKeys { get { return listKeys; } }

        internal string code;
        public string ModelCode {get{return ToString(); }}
        
        public override string ToString()
        {
            switch (code.Substring(0, 1))
            {
                case "a":
                    return "kursy średnich walut obcych";
                case "b":
                    return "kursy średnich walut niewymienialnych";
                case "c":
                    return "kursy kupna i sprzedaży";
                case "h":
                    return "kursy jednostek rozliczeniowych";
                default:
                    return code.Substring(0, 1);
            }
        }
    }
}
