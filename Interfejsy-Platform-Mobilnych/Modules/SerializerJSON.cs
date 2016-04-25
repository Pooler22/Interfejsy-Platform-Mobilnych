using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class SerializerJson
    {

        public static string Serialize<T>(T data)
        {
            var js = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream();
            js.WriteObject(ms, data);
            ms.Position = 0;
            var sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }

        public static T Deserialize<T>(string data)
        {
            var js = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            return (T)js.ReadObject(ms);
        }

    }
}
