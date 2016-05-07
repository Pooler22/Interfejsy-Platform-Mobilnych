using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfejsy_Platform_Mobilnych.Models;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public static class Downloader
    {
        public static async Task<string> DownloadXml(string uri)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var get = await Get(uri);
            return Encoding.GetEncoding("ISO-8859-2").GetString(get, 0, get.Length);
        }

        internal static async Task<List<Position>> GetPositionsFromCode(string code)
        {
            var positions = new List<Position>();
            
            DateTime date;
            date = code.Contains("LastA")
                ? DateTime.Today
                : new DateTime(int.Parse("20" + code.Substring(5, 2)), int.Parse(code.Substring(7, 2)),
                    int.Parse(code.Substring(9, 2)));
                
            if (Storage.IsFile(code))
            {
                positions.AddRange(DeserializerXml.Deserialize(date, Storage.ReadFile(code)));
            }
            else
            {
                if (Connection.IsInternet())
                {
                    const string patternUrl = "http://www.nbp.pl/kursy/xml/";
                    const string patternFileExtension = ".xml";

                    var output = await DownloadXml(patternUrl + code + patternFileExtension);
                    positions.AddRange(DeserializerXml.Deserialize(date, output));
                    await Storage.SaveFile(code, output);
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
    }
}