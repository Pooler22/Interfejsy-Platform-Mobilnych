using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class Archive : Page
    {
        public List<string> model = new List<string>();
        public Archive()
        {
            InitializeComponent();
            model.Add("1");
            model.Add("2");
            model.Add("3");
        }
    }
}
