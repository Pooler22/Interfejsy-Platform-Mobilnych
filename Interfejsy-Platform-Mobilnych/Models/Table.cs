using System.Runtime.Serialization;

namespace Interfejsy_Platform_Mobilnych.Models
{
    [DataContract]
    class Table
    {
        [DataMember]
        internal string code;
        public string Code { get { return code; } }
        
        [DataMember]
        internal bool isDownloaded;
        public bool IsDownloaded { get { return isDownloaded; } }

        public Table(string initCode)
        {
            isDownloaded = false;
            code = initCode;
        }

        public void setDownloaded()
        {
            isDownloaded = true;
        }
    }
}