using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Table
    {
        public string thisTable
        {
            get
            {
                return ToString();
            }
        }
        private string table;
        public string Code { get { return table; } }

        public Table(string table)
        {
            this.table = table;
        }
        
        public List<Position> positions = new List<Position>();
        public List<Position> Positions { get { return positions; } }

        public override string ToString()
        {
            switch (table.Substring(0, 1))
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
                    return table.Substring(0, 1);

            }
        }
    }
}
