using Interfejsy_Platform_Mobilnych.Modules;
using System.Linq;
using System.Text;

namespace Interfejsy_Platform_Mobilnych.Models
{
    abstract class Position
    {
        public string name { get; set; }

        public string Name
        {
            get
            {
                return name.ToString();
            }
        }

        public string ThisPosition
        {
            get
            {
                return ToString();
            }
        }
    }

    class PositionA : Position
    {
        public string converter;
        public string code;
        public string averageRate;

        public override string ToString()
        {
            return name + "\t"+ converter + "\t"+ code + "\t"+ averageRate;
        }
    }

    class PositionB : Position
    {
        public string converter;
        public string code;
        public string averageRate;

        public override string ToString()
        {
            return name + "\t"+ converter + "\t"+ code + "\t"+ averageRate;
        }
    }

    class PositionC : Position
    {
        public string code;
        public string converter;
        public string buyingRate;
        public string sellingRate;

        public override string ToString()
        {
            return name + "\t"+ converter + "\t"+ code + "\t"+ buyingRate + "\t"+ sellingRate;
        }
    }

    class PositionH : Position
    {
        public string nameCountry;
        public string symbol;
        public string converter;
        public string buyingRate;
        public string sellingRate;
        public string averageRate;

        public override string ToString()
        {
            return nameCountry + "\t"+ symbol + "\t"+ name + "\t"+ converter + "\t"+ buyingRate + "\t"+ sellingRate + "\t"+ averageRate;
        }
    }
}
