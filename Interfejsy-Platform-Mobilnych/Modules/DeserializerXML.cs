using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class DeserializerXML
    {
        internal static IEnumerable<Position> deserialize(string code, string xmlString)
        {
            XDocument loadedData = XDocument.Parse(xmlString);
            string position = "pozycja";
            return (from query in loadedData.Descendants(position) select new Position(getList(query.Elements())));
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
