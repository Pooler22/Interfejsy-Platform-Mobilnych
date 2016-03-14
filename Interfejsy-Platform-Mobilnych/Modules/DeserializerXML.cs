using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class DeserializerXML
    {
        internal static Table deserialize(ObservableCollection<Position> defaultPositions, Table table, string a)
        {
            //string peopleXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, input);
            XDocument loadedData = XDocument.Parse(a);
            loadedData.Declaration = new XDeclaration("1.0", "ISO-8859-2", null);

            //Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            
            IEnumerable<Position> pos = getPositions(table.code.Substring(0, 1), loadedData);
            
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
                           select new Position();
                                     //name = (string)query.Element("nazwa_waluty"),
                default:
                    return null;
            }
        }
    }
}
