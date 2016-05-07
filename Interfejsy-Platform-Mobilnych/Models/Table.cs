using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Table
    {
        [DataMember] private readonly string _code;

        public string Code => _code;

        public Table(string initCode)
        {
            _code = initCode;
        }
        
        public string GetDate()
        {
            return _code.Substring(5);
        }

        public int GetNumber()
        {
            return int.Parse(_code.Substring(1, 3));
        }
    }
}