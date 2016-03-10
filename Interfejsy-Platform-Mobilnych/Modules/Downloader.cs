using Interfejsy_Platform_Mobilnych.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public sealed class Downloader
    {
        public static async Task<string> Get(string uri)
        {
            try { 
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
