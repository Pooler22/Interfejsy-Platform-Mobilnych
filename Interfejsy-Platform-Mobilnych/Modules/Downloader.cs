using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public sealed class Downloader
    {
        public static async Task<string> GetString(string uri)
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
            catch (Exception e)
            {
                //to do: obsługa błędu
                return null;
            }
        }

        public static async Task<string> Get1(string uri)
        {
            try
            {
                return await Task.Run(async () => await new HttpClient().GetStringAsync(uri));
            }
            catch (Exception e)
            {
                //to do: obsługa błędu
                return null;
            }
        }
    }
}
