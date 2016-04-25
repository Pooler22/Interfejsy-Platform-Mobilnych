using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Interfejsy_Platform_Mobilnych.Models;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class DeserializerXml
    {
        internal static IEnumerable<Position> Deserialize(string xmlString)
        {
            var loadedData = XDocument.Parse(xmlString);
            const string position = "pozycja";
            return from query in loadedData.Descendants(position) select 
                   new Position(
                       GetList(query.Elements())[0], 
                       int.Parse(GetList(query.Elements())[1]), 
                       GetList(query.Elements())[2], 
                       double.Parse(GetList(query.Elements())[3])
                       );
        }

        private static List<string> GetList(IEnumerable<XElement> input)
        {
            return input.Select(element => element.Value).ToList();
        }
    }
}