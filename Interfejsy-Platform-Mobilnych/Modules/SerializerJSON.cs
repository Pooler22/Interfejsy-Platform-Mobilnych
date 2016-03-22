using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class SerializerJSON
    {
        public class Serializer
        {
            public static string serialize<T>(T data)
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, data);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }

            public static T deserialize<T>(string data)
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
                return (T)js.ReadObject(ms);
            }
        }
    }
}
