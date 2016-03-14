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
            Encoding iso_8859_2 = Encoding.UTF7;
            byte[] get = await Get(uri);
            return iso_8859_2.GetString(get, 0, get.Length);

            //byte[] get = await Get(uri);
            //byte[] data = Convert.FromBase64String(await Get1(uri));
            //return Encoding.UTF8.GetString(data);


            //string str = await Get1(uri);
            //str = decodeXML(str);
            //return await Get1(uri);
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

        private static readonly ushort[] iso8859_2 = {
        0x00A0, 0x0104, 0x02D8, 0x0141, 0x00A4, 0x013D, 0x015A, 0x00A7, 0x00A8, 0x0160,
        0x015E, 0x0164, 0x0179, 0x00AD, 0x017D, 0x017B, 0x00B0, 0x0105, 0x02DB, 0x0142,
        0x00B4, 0x013E, 0x015B, 0x02C7, 0x00B8, 0x0161, 0x015F, 0x0165, 0x017A, 0x02DD,
        0x017E, 0x017C, 0x0154, 0x00C1, 0x00C2, 0x0102, 0x00C4, 0x0139, 0x0106, 0x00C7,
        0x010C, 0x00C9, 0x0118, 0x00CB, 0x011A, 0x00CD, 0x00CE, 0x010E, 0x0110, 0x0143,
        0x0147, 0x00D3, 0x00D4, 0x0150, 0x00D6, 0x00D7, 0x0158, 0x016E, 0x00DA, 0x0170,
        0x00DC, 0x00DD, 0x0162, 0x00DF, 0x0155, 0x00E1, 0x00E2, 0x0103, 0x00E4, 0x013A,
        0x0107, 0x00E7, 0x010D, 0x00E9, 0x0119, 0x00EB, 0x011B, 0x00ED, 0x00EE, 0x010F,
        0x0111, 0x0144, 0x0148, 0x00F3, 0x00F4, 0x0151, 0x00F6, 0x00F7, 0x0159, 0x016F,
        0x00FA, 0x0171, 0x00FC, 0x00FD, 0x0163, 0x02D9 };

        public static string decodeXML(string xml)
        {
            byte[] data = Convert.FromBase64String(xml);
            char[] decoded = new char[data.Length];

            for (int i = 0; i < data.Length; i++)
                if (data[i] < 128)
                    decoded[i] = (char)data[i];
                else if (data[i] < 0xA0)
                    decoded[i] = '\0';
                else
                    decoded[i] = (char)iso8859_2[data[i] - 0xA0];

            return new string(decoded);
        }

    }
}
