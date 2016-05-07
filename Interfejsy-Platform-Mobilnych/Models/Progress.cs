namespace Interfejsy_Platform_Mobilnych.Models
{
    internal class Progress
    {
        public readonly int Maximum;
        public readonly int Minimum;
        public int Value;

        public Progress(int min, int max)
        {
            Minimum = Value = min;
            Maximum = max;
        }
    }
}