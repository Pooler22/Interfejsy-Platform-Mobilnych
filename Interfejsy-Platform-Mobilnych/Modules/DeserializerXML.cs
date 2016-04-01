using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class DeserializerXML
    {
        internal static Table deserialize(string code, string xmlString)
        {
            Table table = new Table(code);
            XDocument loadedData = XDocument.Parse(xmlString);
            string position = "pozycja";
            
            foreach (XElement element in loadedData.Descendants(position).First().Elements())
            {
                //to do: wielka pierwsza litera 
                table.listKeys.Add(element.Name.ToString().Replace('_',' '));
            }

            table.positions = (from query in loadedData.Descendants(position)
                               select new Position() { listValues = getList(query.Elements()) })
                               .ToList();
            return table;
        }

        internal static List<string> getList(IEnumerable<XElement> input)
        {
            List<string> list = new List<string>();
            foreach (XElement element in input)
            {
                list.Add(element.Value);
            }
            return list;
        }
    }
}
