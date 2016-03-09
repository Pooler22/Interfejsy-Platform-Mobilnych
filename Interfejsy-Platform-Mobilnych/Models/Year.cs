using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Models
{
    class Year
    {
        public int year { get; }
        public Day days;

        public Year(int year, string file)
        {
            this.year = year;
        }

        public string GetYear()
        {
            return year.ToString();   
        }
    }
}
