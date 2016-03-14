using Interfejsy_Platform_Mobilnych.Models;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class DeserializerXML
    {
        internal static Table deserialize(string code, string xmlString)
        {
            Table table = new Table() { code = code };
            XDocument loadedData = XDocument.Parse(xmlString);
            loadedData.Declaration = new XDeclaration("1.0", "ISO-8859-2", null);

            XElement current = loadedData.Descendants("pozycja").First();

            foreach (XElement element in current.Elements())
            {
                table.listKeys.Add(element.Name.ToString());
            }
            table.positions = (from query in loadedData.Descendants("pozycja")
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
