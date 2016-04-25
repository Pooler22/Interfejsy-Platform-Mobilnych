using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    internal class Table
    {
        [DataMember] private readonly string code;
        public string Code => code;

        [DataMember] private bool _isDownloaded;
        public bool IsDownloaded => _isDownloaded;

        public Table(string initCode)
        {
            _isDownloaded = false;
            code = initCode;
        }

        public void SetDownloaded()
        {
            _isDownloaded = true;
        }

        public string GetDate()
        {
            return code.Substring(5);
        }

        public int GetNumber()
        {
            return int.Parse(code.Substring(1, 3));
        }
    }
}