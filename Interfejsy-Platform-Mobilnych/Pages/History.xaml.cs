using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class History : Page
    {
        DatabaseViewModel ViewModel { get; set; }

        public History()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
        }

        private void Date_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if(StartDate.Date < StartDate.Date)
            {
                GenerateButton.IsEnabled = true;
                InfoTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                GenerateButton.IsEnabled = false;
                InfoTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
