using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private void CalendarDatePicker_OnCalendarViewDayItemChanging(CalendarView sender,
            CalendarViewDayItemChangingEventArgs args)
        {
            ViewModel.CheckBlackout(args);
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Generate(StartDate.Date, EndDate.Date);
        }

        private void Date_DateChanged(CalendarDatePicker calendarDatePicker, CalendarDatePickerDateChangedEventArgs args)
        {
            if (StartDate.Date < EndDate.Date)
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
    }
}