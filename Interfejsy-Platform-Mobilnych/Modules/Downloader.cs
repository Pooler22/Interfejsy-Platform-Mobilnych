using Interfejsy_Platform_Mobilnych.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public static class Downloader
    {
        public static async Task<string> DownloadXml(string uri)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] get = await Get(uri);
            return Encoding.GetEncoding("ISO-8859-2").GetString(get, 0, get.Length);
        }

        internal static async Task<List<Position>> GetPositionsFromCode(string code)
        {
            Storage storage = new Storage();
            List<Position> positions = new List<Position>();

            if (Storage.IsFile(code) && code != null)
            {
                await storage.CreateFile(code);
                storage.ReadFile(code);
                foreach (Position pos in DeserializerXml.Deserialize(storage.ReadStringFromFile()))
                {
                    positions.Add(pos);
                }
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string patternUrl = "http://www.nbp.pl/kursy/xml/";
                    string patternFileExtension = ".xml";

                    string output = await DownloadXml(patternUrl + code + patternFileExtension);
                    foreach (Position pos in DeserializerXml.Deserialize(output))
                    {
                        positions.Add(pos);
                    }
                    await storage.CreateFile(code);
                    storage.SaveFile(code, output);
                }
                else
                {
                }
            }
            return positions;
        }

        private static async Task<byte[]> Get(string uri)
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

/*
        internal static async Task<List<Position>> GetFile(string code)
        {
            string patternUrl = "http://www.nbp.pl/kursy/xml/";
            string patternFileExtension = ".xml";
            List<Position> positions = new List<Position>();
            Storage storage = new Storage();

            if (Storage.IsFile(code) && code != null)
            {
                await storage.CreateFile(code);
                storage.ReadFile(code);
                DeserializerXml.Deserialize(storage.ReadStringFromFile()).ToList().ForEach((x) => positions.Add(x));
            }
            else
            {
                if (Connection.IsInternet())
                {
                    string output = await DownloadXml(patternUrl + code + patternFileExtension);
                    await storage.CreateFile(code);
                    storage.SaveFile(code, output);
                    DeserializerXml.Deserialize(output).ToList().ForEach((x) => positions.Add(x));                    
                }
            }
            return positions;
        }
*/
    }
}
