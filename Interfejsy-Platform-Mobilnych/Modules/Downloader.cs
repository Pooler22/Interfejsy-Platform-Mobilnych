using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    public sealed class Downloader : IBackgroundTask
    {
        public delegate string MyDelegate(string ur);

        static public string Get(string url)
        {
            Task<string> task = GetAsync(url);
            task.Wait();
            return task.Result;
        }

        static async Task<string> GetAsync(string uri)
        {
            var content = await new HttpClient().GetStringAsync(uri);
            return await Task.Run(() => content);
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();
        }
    }
}
