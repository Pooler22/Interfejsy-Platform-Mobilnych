using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class DeserializerXML
    {
        internal static Table deserialize(ObservableCollection<Position> defaultPositions, Table table, string a)
        {
            //string peopleXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, input);
            XDocument loadedData = XDocument.Parse(a);
            //loadedData.Declaration = new XDeclaration("1.0", "ISO-8859-1", "yes");

            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            var data1 = from query in loadedData.Descendants("pozycja")
                        select new Position
                       { 
                            name = (string)query.Element("nazwa_waluty"),
                            converter = (string)query.Element("przelicznik"),
                            code = (string)query.Element("kod_waluty"),
                            buyingRate = (string)query.Element("kurs_kupna"),
                            sellingRate = (string)query.Element("kurs_sprzedazy")
                        };

            foreach (var i in data1)
            {
                (table).positions.Add(i);
                defaultPositions.Add(i);
            }

            return table;
        }
    }
}
