using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

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
            IEnumerable<Position> pos = getPositions(table.Code.Substring(0, 1), loadedData);
            foreach (Position i in pos)
            {
                (table).positions.Add(i);
                defaultPositions.Add(i);
            }
            return table;
        }


        static public IEnumerable<Position> getPositions(string code, XDocument loadedData)
        {
            switch (code)
            {
                case "a":
                    return from query in loadedData.Descendants("pozycja")
                                     select new PositionA
                                     {
                                         name = (string)query.Element("nazwa_waluty"),
                                         converter = (string)query.Element("przelicznik"),
                                         code = (string)query.Element("kod_waluty"),
                                         averageRate = (string)query.Element("kurs_kupna")
                                     };
                case "b":
                    return from query in loadedData.Descendants("pozycja")
                                     select new PositionB
                                     {
                                         name = (string)query.Element("nazwa_waluty"),
                                         converter = (string)query.Element("przelicznik"),
                                         code = (string)query.Element("kod_waluty"),
                                         averageRate = (string)query.Element("kurs_kupna")
                                     };
                case "c":
                    return from query in loadedData.Descendants("pozycja")
                                     select new PositionC
                                     {
                                         name = (string)query.Element("nazwa_waluty"),
                                         converter = (string)query.Element("przelicznik"),
                                         code = (string)query.Element("kod_waluty"),
                                         buyingRate = (string)query.Element("kurs_kupna"),
                                         sellingRate = (string)query.Element("kurs_kupna")
                                     };
                case "h":
                    return from query in loadedData.Descendants("pozycja")
                                     select new PositionH
                                     {
                                         name = (string)query.Element("nazwa_waluty"),
                                         converter = (string)query.Element("przelicznik"),
                                         nameCountry = (string)query.Element("kurs_kupna"),
                                         symbol = (string)query.Element("kurs_kupna"),
                                         buyingRate = (string)query.Element("kurs_kupna"),
                                         sellingRate = (string)query.Element("kurs_kupna"),
                                         averageRate = (string)query.Element("kurs_kupna")
                                     };
                default:
                    return null;
            }
        }
    }
}
