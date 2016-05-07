using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Interfejsy_Platform_Mobilnych.Models;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class DeserializerXml
    {
        internal static IEnumerable<Position> Deserialize(DateTime date,string xmlString)
        {
            const string position = "pozycja";
            var loadedData = XDocument.Parse(xmlString);
            return from query in loadedData.Descendants(position)
                select PositionConvert(date ,query.Descendants().ToList());
        }

        private static Position PositionConvert(DateTime date, IReadOnlyList<XElement> query)
        {
            return new Position(
                date, 
                query[0].Value,
                int.Parse(query[1].Value),
                query[2].Value,
                double.Parse(query[3].Value.Replace(',', '.'))
                );
        }
    }
}