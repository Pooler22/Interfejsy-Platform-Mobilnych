namespace Interfejsy_Platform_Mobilnych.Models
{
    internal class Progress
    {
        public readonly int DownloadedMinimum;
        public readonly int DownloadedMaximum;
        public int DownloadedValue;  

        public Progress(int a, int b)
        {
            DownloadedMinimum = a;
            DownloadedMaximum = b;
            DownloadedMinimum = a;
        }
    }
}
