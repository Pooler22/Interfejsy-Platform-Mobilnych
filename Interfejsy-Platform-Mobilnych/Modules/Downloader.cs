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
            Encoding iso_8859_2 = Encoding.GetEncoding("ISO-8859-2");
            byte[] get = await Get(uri);
            return iso_8859_2.GetString(get, 0, get.Length);
        }

        public static async Task<byte[]> Get(string uri)
        {
            try { 
            var content = await new HttpClient().GetByteArrayAsync(uri);
                return await Task.Run(() => content);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<string> Get1(string uri)
        {
            try
            {
                var content = await new HttpClient().GetStringAsync(uri);
                return await Task.Run(() => content);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
