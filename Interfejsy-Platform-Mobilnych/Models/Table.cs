using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Table
    {
        [DataMember] private readonly string _code;
        public string Code => _code;

        [DataMember] private bool _isDownloaded;
        public bool IsDownloaded => _isDownloaded;

        public Table(string initCode)
        {
            _isDownloaded = false;
            _code = initCode;
        }

        public void SetDownloaded()
        {
            _isDownloaded = true;
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