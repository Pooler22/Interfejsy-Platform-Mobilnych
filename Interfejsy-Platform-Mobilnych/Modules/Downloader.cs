using Interfejsy_Platform_Mobilnych.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public sealed class Downloader
    {
        public static async Task<string> DownloadXml(string uri)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] get = await Get(uri);
            return Encoding.GetEncoding("ISO-8859-2").GetString(get, 0, get.Length);
        }

        public static async Task<byte[]> Get(string uri)
        {
            try
            {
                return await Task.Run(async () => await new HttpClient().GetByteArrayAsync(uri));
            }
            catch (Exception)
            {
                //to do: obsługa błędu
                return null;
            }
        }

        public static async Task<string> DownloadString(string uri)
        {
            try
            {
                return await Task.Run(async () => await new HttpClient().GetStringAsync(uri));
            }
            catch (Exception)
            {
                //to do: obsługa błędu
                return null;
            }
        }

        internal static async Task<List<Position>> getFile(string code)
        {
            string patternURL = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            List<Position> positions = new List<Position>();
            Storage storage = new Storage();

            if (storage.IsFile(code) && code != null)
            {
                await storage.createFile(code);
                storage.readFile(code);
                DeserializerXML.deserialize(storage.readStringFromFile()).ToList().ForEach((x) => positions.Add(x));
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string output = await DownloadXml(patternURL + code + patternFileExtension);
                    await storage.createFile(code);
                    storage.saveFile(code, output);
                    DeserializerXML.deserialize(output).ToList().ForEach((x) => positions.Add(x));                    
                }
            }
            return positions;
        }
    }
}
