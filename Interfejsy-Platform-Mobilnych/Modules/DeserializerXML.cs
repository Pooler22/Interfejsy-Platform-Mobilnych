using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class DeserializerXml
    {
        internal static IEnumerable<Position> Deserialize(string xmlString)
        {
            var loadedData = XDocument.Parse(xmlString);
            const string position = "pozycja";
            return (from query in loadedData.Descendants(position) select new Position(GetList(query.Elements())));
        }
        
        private static List<string> GetList(IEnumerable<XElement> input)
        {
            return input.Select(element => element.Value).ToList();
        }
    }
}
