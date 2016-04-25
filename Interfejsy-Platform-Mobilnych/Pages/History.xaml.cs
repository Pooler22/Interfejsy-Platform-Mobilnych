using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Interfejsy_Platform_Mobilnych.ViewModel;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class History
    {
        public History()
        {
            InitializeComponent();
        }

        private DatabaseViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Generate(StartDate.Date, EndDate.Date);
        }

        /*
                private void Date_DateChanged(object sender, DatePickerValueChangedEventArgs e)
                {
                    if(StartDate.Date < EndDate.Date)
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
        */

        /*
                private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
                {

                }
        */
    }
}